using ShopCommons.Models;

namespace ShopCommons.Services.Int
{
    public interface IRabbitServiceHelper
    {
        void CreateQueue(string queueName);
        void EnqueueMessage<T>(string queueName, RabbitMQMessageRequest<T> messageData);
        void StartListening(Func<string, Task<string>> processRequestCallback);
        void StartListeningRpc();
        Task<byte[]> SendRPC<T>(string queueNameDestination, RabbitMQMessageRequest<T> messageData, CancellationToken cancellationToken);
    }
}
