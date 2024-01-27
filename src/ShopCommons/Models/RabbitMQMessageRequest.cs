using ShopCommons.Models.Enum;

namespace ShopCommons.Models
{
    public class RabbitMQMessageRequest<T>
    {
        public RabbitMQMessageType MessageType { get; set; }
        public T MessageData { get; set; }

        public RabbitMQMessageRequest(RabbitMQMessageType messageType, T messageData)
        {
            this.MessageType = messageType;
            this.MessageData = messageData;
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequestFactory(RabbitMQMessageType messageType, T messageData)
        {
            return new RabbitMQMessageRequest<T>(messageType, messageData);
        }
    }
}
