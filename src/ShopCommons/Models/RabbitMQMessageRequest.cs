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

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_ORDER_DETAILS_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_ORDER_DETAILS, messageData);
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_CREATE_ORDER_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_CREATE_ORDER, messageData);
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_DELETE_ORDER_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_DELETE_ORDER, messageData);
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_PRODUCTS_LIST_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_PRODUCTS_LIST, messageData);
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_GET_PRODUCT_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_GET_PRODUCT, messageData);
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_CREATE_PRODUCT_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_CREATE_PRODUCT, messageData);
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_DELETE_PRODUCT_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_DELETE_PRODUCT, messageData);
        }

        public static RabbitMQMessageRequest<T> RabbitMQMessageRequest_EDIT_PRODUCT_Factory(T messageData)
        {
            return new RabbitMQMessageRequest<T>(RabbitMQMessageType.REQUEST_EDIT_PRODUCT, messageData);
        }
    }
}
