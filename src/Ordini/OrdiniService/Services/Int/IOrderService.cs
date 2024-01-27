using OrdiniService.Models.API;

namespace OrdiniService.Services.Int
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetOrders(CancellationToken cancellationToken);
        Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken);
    }
}
