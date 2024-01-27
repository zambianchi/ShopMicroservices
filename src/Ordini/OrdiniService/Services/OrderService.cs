using Microsoft.EntityFrameworkCore;
using OrdiniService.Context;
using OrdiniService.Models.API;
using OrdiniService.Services.Int;

namespace OrdiniService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _ordiniContext;

        public OrderService(OrderContext ordiniContext)
        {
            this._ordiniContext = ordiniContext;
        }

        public async Task<List<OrderDTO>> GetOrders(CancellationToken cancellationToken)
        {
            List<OrderDTO> orders = [];

            var ordersDB = await _ordiniContext.Orders
                .ToListAsync();

            foreach(var orderDB in ordersDB)
            {
                orders.Add(OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId));
            }

            return orders;
        }

        public async Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            var orderDB = await _ordiniContext.Orders
                .Where(x => x.Id == idOrder)
                .SingleAsync(cancellationToken);

            return OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId);
        }
    }
}
