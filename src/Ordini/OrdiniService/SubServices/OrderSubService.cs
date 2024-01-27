using RabbitMQ.Client;
using ShopCommons.Models;
using ShopCommons.Services.Int;

namespace OrdiniService.SubServices
{
    public class OrderSubService
    {
        private readonly IRabbitServiceHelper _rabbitServiceHelper;

        public OrderSubService(IRabbitServiceHelper rabbitServiceHelper)
        {
            this._rabbitServiceHelper = rabbitServiceHelper;
        }

        public void GetOrderProducts(List<long> productsId)
        {
            var request = RabbitMQMessageRequest<List<long>>.RabbitMQMessageRequestFactory(ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_PRODOTTI_DA_LISTA_ID, productsId); 
            _rabbitServiceHelper.EnqueueMessage("ProdottiService_Queue", request);
        }
    }
}
