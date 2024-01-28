using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrdiniService.Context;
using OrdiniService.Models;
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
                .Include(x => x.OrderProducts)
                .ToListAsync();

            foreach (var orderDB in ordersDB)
            {
                var orderProductIds = orderDB.OrderProducts
                    .Select(x => x.IdProduct)
                    .ToList();

                orders.Add(OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, orderProductIds));
            }

            return orders;
        }

        public async Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            var orderDB = await _ordiniContext.Orders
                .Include(x => x.OrderProducts)
                .Where(x => x.Id == idOrder)
                .SingleAsync(cancellationToken);

            var orderProductIds = orderDB.OrderProducts
                .Select(x => x.IdProduct)
                .ToList();

            return OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, orderProductIds);
        }

        public async Task<OrderDTO> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if(request.ProductIds.IsNullOrEmpty())
            {
                throw new Exception("Nessun articolo da associare al nuovo ordine");
            }

            var orderProducts = request.ProductIds
                .Select(x => new OrderProducts
                {
                    IdProduct = x
                })
                .ToList();

            var orderDB = new Order
            {
                Date = request.DataOrdine,
                CreationAccountId = request.UserId,
                OrderProducts = orderProducts
            };

            await _ordiniContext.Orders
                .AddAsync(orderDB, cancellationToken);

            await _ordiniContext.SaveChangesAsync(cancellationToken);

            var orderProductIds = orderDB.OrderProducts
                .Select(x => x.IdProduct)
                .ToList();

            return OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, orderProductIds);
        }
    }
}
