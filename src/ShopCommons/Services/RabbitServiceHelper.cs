using RabbitMQ.Client;
using ShopCommons.Models;
using ShopCommons.Services.Int;
using System.Text;
using System.Text.Json;

namespace ShopCommons.Services
{
    public class RabbitServiceHelper : IRabbitServiceHelper
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitServiceHelper(string hostname, int port, string username, string password, string queueName)
        {
            this._factory = new ConnectionFactory() { HostName = hostname, Port = port };
            this._factory.UserName = username;
            this._factory.Password = password;
            this._connection = this._factory.CreateConnection();
            this._channel = this._connection.CreateModel();

            this.CreateQueue(queueName);
        }

        public void CreateQueue(string queueName)
        {
            this._channel.QueueDeclare(queue: queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }

        public void EnqueueMessage<T>(string queueName, RabbitMQMessageRequest<T> messageData)
        {
            var messageBody = JsonSerializer.Serialize(messageData);
            var messageBodyBytes = Encoding.UTF8.GetBytes(messageBody);

            this._channel.BasicPublish(exchange: string.Empty,
                     routingKey: queueName,
                     basicProperties: null,
                     body: messageBodyBytes);
        }
    }
}
