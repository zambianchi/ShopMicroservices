using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;

namespace ApiGateway.Services.Int
{
    public interface IApiGatewayOrderService
    {
        Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken);
        Task<OrderDTO> CreateOrder(CreateOrderRequestDTO request, CancellationToken cancellationToken);
        Task DeleteOrder(long idOrder, CancellationToken cancellationToken);
    }
}
