using OrdiniService.Models.API.Entity;
using OrdiniService.Models.API.Request;

namespace OrdiniService.Services.Int
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetOrders(CancellationToken cancellationToken);
        Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken);
        Task<OrderDTO> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken);
        Task DeleteOrder(long idOrder, CancellationToken cancellationToken);
    }
}
