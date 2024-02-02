using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopCommons.Models;
using ShopCommons.Services.Int;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace ShopCommons.Services
{
    public class RabbitServiceHelper : IRabbitServiceHelper
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly string _queueName;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<byte[]>> callbackMapper = new();

        public RabbitServiceHelper(string hostname, int port, string username, string password, string queueName)
        {
            this._factory = new ConnectionFactory() { HostName = hostname, Port = port };
            this._factory.UserName = username;
            this._factory.Password = password;
            this._connection = this._factory.CreateConnection();
            this._channel = this._connection.CreateModel();

            this._queueName = queueName;

            // Inizializza la coda
            this.CreateQueue(queueName);
        }

        /// <summary>
        /// Dichiara la coda RabbitMQ
        /// </summary>
        /// <param name="queueName">Nome coda</param>
        public void CreateQueue(string queueName)
        {
            this._channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            this._channel.BasicQos(0, 1, false);
        }

        /// <summary>
        /// Inoltra messaggio sulla coda
        /// </summary>
        /// <typeparam name="T">Tipo dato messaggio</typeparam>
        /// <param name="queueName">Nome coda</param>
        /// <param name="messageData">Richiesta</param>
        public void EnqueueMessage<T>(string queueName, RabbitMQMessageRequest<T> messageData)
        {
            var messageBody = JsonSerializer.Serialize(messageData);
            var messageBodyBytes = Encoding.UTF8.GetBytes(messageBody);

            // Pubblica richiesta sulla coda
            this._channel.BasicPublish(exchange: string.Empty,
                routingKey: queueName,
                basicProperties: null,
                body: messageBodyBytes);
        }

        /// <summary>
        /// Avvia l'ascolto sulla coda specifica
        /// </summary>
        /// <param name="processRequestCallback">Callback della chiamata sulla coda</param>
        public void StartListening(Func<string, Task<string>> processRequestCallback)
        {
            this._channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(this._channel);
            this._channel.BasicConsume(queue: this._queueName,
                                 autoAck: false,
                                 consumer: consumer);

            consumer.Received += async (model, ea) =>
            {
                string response = string.Empty;

                var body = ea.Body.ToArray();
                var props = ea.BasicProperties;
                var replyProps = this._channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    // Prendo il messaggio e lo rimando al callback per gestirlo
                    var message = Encoding.UTF8.GetString(body);
                    response = await processRequestCallback(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine($" [.] {e.Message}");
                    response = string.Empty;
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    this._channel.BasicPublish(exchange: string.Empty,
                                         routingKey: props.ReplyTo,
                                         basicProperties: replyProps,
                                         body: responseBytes);

                    // Invio l'ACK per messaggio ricevuto
                    this._channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };
        }

        /// <summary>
        /// Sai in ascolto per messaggi RCP
        /// </summary>
        public void StartListeningRpc()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                    return;

                var body = ea.Body.ToArray();

                tcs.TrySetResult(body);
            };

            // "Consuma" messaggio sulla coda
            this._channel.BasicConsume(
                queue: this._queueName,
                autoAck: true,
                consumer: consumer
            );
        }

        /// <summary>
        /// Invia messaggio RPC sulla coda di destinazione specifica
        /// </summary>
        /// <typeparam name="T">Tipo dato messaggio</typeparam>
        /// <param name="queueNameDestination">Nome coda</param>
        /// <param name="messageData">Richiesta</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Risposta al messaggio RPC</returns>
        public async Task<byte[]> SendRPC<T>(string queueNameDestination, RabbitMQMessageRequest<T> messageData, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid().ToString();

            var props = this._channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = this._queueName;

            var messageBody = JsonSerializer.Serialize(messageData);
            var messageBodyBytes = Encoding.UTF8.GetBytes(messageBody);

            // Invia sulla coda il messaggio
            this._channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueNameDestination,
                basicProperties: props,
                body: messageBodyBytes
            );

            var tcs = new TaskCompletionSource<byte[]>();
            callbackMapper.TryAdd(correlationId, tcs);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                // Se il messsaggio di risposta è relativo al mio inviato
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    var body = ea.Body.ToArray();
                    tcs.SetResult(body);
                }
            };

            cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));
            return await tcs.Task;
        }
    }
}
