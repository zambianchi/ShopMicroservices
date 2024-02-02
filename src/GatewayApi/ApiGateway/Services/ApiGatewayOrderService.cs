using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;
using ApiGateway.Services.Int;
using ApiGateway.SubService.Int;

namespace ApiGateway.Services
{
    public class ApiGatewayOrderService : IApiGatewayOrderService
    {
        private readonly IApiGatewayOrdiniSubService _apiGatewaySubService;

        public ApiGatewayOrderService(IApiGatewayOrdiniSubService apiGatewaySubService)
        {
            _apiGatewaySubService = apiGatewaySubService;
        }

        public async Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            var getOrderResult = await _apiGatewaySubService.GetOrder(idOrder, cancellationToken);
            return getOrderResult;
        }

        public async Task<OrderDTO> CreateOrder(CreateOrderRequestDTO request, CancellationToken cancellationToken)
        {
            var createOrderResult = await _apiGatewaySubService.CreateOrder(request, cancellationToken);
            return createOrderResult;
        }

        public async Task DeleteOrder(long idOrder, CancellationToken cancellationToken)
        {
            await _apiGatewaySubService.DeleteOrder(idOrder, cancellationToken);
        }
    }
}
