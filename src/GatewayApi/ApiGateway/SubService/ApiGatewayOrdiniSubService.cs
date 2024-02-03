using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;
using ApiGateway.Models.ExternalAPI.Entity;
using ApiGateway.Models.ExternalAPI.Response;
using ApiGateway.SubService.Int;
using ShopCommons.Helper;
using ShopCommons.Models;
using System.Collections.Generic;

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
            var productsId = getOrderDetailsResponse.Products
                .Select(x => x.IdProduct)
                .ToList();
            var orderProducts = await this.GetSpecificProducts(productsId, cancellationToken);

            var order = OrderDTO.OrderDTOFactory(getOrderDetailsResponse.Id, getOrderDetailsResponse.Date, getOrderDetailsResponse.CreationAccountId, orderProducts);
            return order;
        }

        /// <summary>
        /// Crea un ordine
        /// </summary>
        public async Task<OrderDTO> CreateOrder(CreateOrderRequestDTO request, CancellationToken cancellationToken)
        {
            var productsId = request.Products.Select(x => x.IdProduct).ToList();

            // Controlla la disponibilità dei prodotti
            var getProductsAvailabilitiesRequest = RabbitMQMessageRequest<List<long>>.RabbitMQMessageRequest_GET_PRODUCTS_AVAILABILITIES_Factory(productsId);

            var getProductsAvailabilitiesResponse = await RabbitServiceMessageWrapper.SendRPC<List<long>, GetSpecificProductsAvailabilitiesApiResponse>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_ProductRabbitMQQueue, getProductsAvailabilitiesRequest, cancellationToken);

            foreach (var product in request.Products)
            {
                var productAvailability = getProductsAvailabilitiesResponse.ProductsAvailabilities
                    .Where(x => x.Id == product.IdProduct)
                    .FirstOrDefault();

                if(productAvailability == null)
                {
                    throw new InvalidOperationException("Prodotto non disponibile");
                }

                if(productAvailability.Availability < product.Amount)
                {
                    throw new InvalidOperationException("Richiesta quantità maggiore della disponibilità");
                }
            }

            var requestCreateOrder = RabbitMQMessageRequest<CreateOrderRequestDTO>.RabbitMQMessageRequest_CREATE_ORDER_Factory(request);

            // Creo l'ordine
            var createOrderResponse = await RabbitServiceMessageWrapper.SendRPC<CreateOrderRequestDTO, OrderApiDTO>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_OrderRabbitMQQueue, requestCreateOrder, cancellationToken);

            // Prendo i prodotti associati all'ordine
            var orderProducts = await this.GetSpecificProducts(productsId, cancellationToken);

            // Creo la lista con i prodotti acquistati dal cliente con le relative quantità corrette
            List<ProductDTO> returnProducts = new List<ProductDTO>();
            foreach (var product in orderProducts)
            {
                var requestProduct = request.Products
                    .Where(x => x.IdProduct == product.IdProduct)
                    .Single();

                var productToAdd = ProductDTO.ProductDTOFactory(product.IdProduct, product.Name, product.Description, requestProduct.Amount, requestProduct.Amount);
                returnProducts.Add(productToAdd);
            }

            var order = OrderDTO.OrderDTOFactory(createOrderResponse.Id, createOrderResponse.Date, createOrderResponse.CreationAccountId, returnProducts);
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
