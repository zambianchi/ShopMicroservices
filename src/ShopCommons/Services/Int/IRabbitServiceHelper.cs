using ShopCommons.Models;

namespace ShopCommons.Services.Int
{
    public interface IRabbitServiceHelper
    {
        void CreateQueue(string queueName);

        void EnqueueMessage<T>(string queueName, RabbitMQMessageRequest<T> messageData);
    }
}
