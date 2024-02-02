using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;
using ApiGateway.Models.ExternalAPI.Entity;
using ApiGateway.SubService.Int;
using ShopCommons.Helper;
using ShopCommons.Models;

namespace ApiGateway.SubService
{
    public partial class ApiGatewaySubService : IApiGatewayOrdiniSubService
    {
        /// <summary>
        /// Carica un ordine
        /// </summary>
        public async Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            var requestOrder = RabbitMQMessageRequest<long>.RabbitMQMessageRequest_ORDER_DETAILS_Factory(idOrder);

            // Prendo l'ordine
            var getOrderDetailsResponse = await RabbitServiceMessageWrapper.SendRPC<long, OrderApiDTO>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_OrderRabbitMQQueue, requestOrder, cancellationToken);

            // Prendo i prodotti associati all'ordine
            var orderProducts = await this.GetSpecificProducts(getOrderDetailsResponse.ProductIds, cancellationToken);

            var order = OrderDTO.OrderDTOFactory(getOrderDetailsResponse.Id, getOrderDetailsResponse.Date, getOrderDetailsResponse.CreationAccountId, orderProducts);
            return order;
        }

        /// <summary>
        /// Crea un ordine
        /// </summary>
        public async Task<OrderDTO> CreateOrder(CreateOrderRequestDTO request, CancellationToken cancellationToken)
        {
            var requestCreateOrder = RabbitMQMessageRequest<CreateOrderRequestDTO>.RabbitMQMessageRequest_CREATE_ORDER_Factory(request);

            // Creo l'ordine
            var createOrderResponse = await RabbitServiceMessageWrapper.SendRPC<CreateOrderRequestDTO, OrderApiDTO>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_OrderRabbitMQQueue, requestCreateOrder, cancellationToken);

            // Prendo i prodotti associati all'ordine
            var orderProducts = await this.GetSpecificProducts(request.ProductIds, cancellationToken);

            var order = OrderDTO.OrderDTOFactory(createOrderResponse.Id, createOrderResponse.Date, createOrderResponse.CreationAccountId, orderProducts);
            return order;
        }

        /// <summary>
        /// Elimina un ordine
        /// </summary>
        public async Task DeleteOrder(long idOrder, CancellationToken cancellationToken)
        {
            var requestDeleteOrder = RabbitMQMessageRequest<long>.RabbitMQMessageRequest_DELETE_ORDER_Factory(idOrder);

            // Elimino l'ordine
            await RabbitServiceMessageWrapper.SendRPC<long, bool>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_OrderRabbitMQQueue, requestDeleteOrder, cancellationToken);
        }
    }
}
