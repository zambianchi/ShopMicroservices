using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrdiniService.Context;
using OrdiniService.Models;
using OrdiniService.Models.API.Entity;
using OrdiniService.Models.API.Request;
using OrdiniService.Models.DB;
using OrdiniService.Services.Int;

namespace OrdiniService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _orderContext;

        public OrderService(OrderContext orderContext)
        {
            this._orderContext = orderContext;
        }

        /// <summary>
        /// Carica tutti gli ordini
        /// </summary>
        public async Task<List<OrderDTO>> GetOrders(CancellationToken cancellationToken)
        {
            List<OrderDTO> orders = [];

            var ordersDB = await _orderContext.Orders
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

        /// <summary>
        /// Carica un ordine
        /// </summary>
        public async Task<OrderDTO> GetOrder(long idOrder, CancellationToken cancellationToken)
        {
            var orderDB = await _orderContext.Orders
                .Include(x => x.OrderProducts)
                .Where(x => x.Id == idOrder)
                .SingleAsync(cancellationToken);

            var orderProductIds = orderDB.OrderProducts
                .Select(x => x.IdProduct)
                .ToList();

            return OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, orderProductIds);
        }

        /// <summary>
        /// Crea un ordine
        /// </summary>
        public async Task<OrderDTO> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request.ProductIds.IsNullOrEmpty())
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

            // Aggiungi il nuovo ordine al DB
            await _orderContext.Orders
                .AddAsync(orderDB, cancellationToken);

            await _orderContext.SaveChangesAsync(cancellationToken);

            var orderProductIds = orderDB.OrderProducts
                .Select(x => x.IdProduct)
                .ToList();

            return OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, orderProductIds);
        }

        /// <summary>
        /// Elimina un ordine
        /// </summary>
        public async Task DeleteOrder(long idOrder, CancellationToken cancellationToken)
        {
            var orderDB = await _orderContext.Orders
                .Where(x => x.Id == idOrder)
                .SingleAsync(cancellationToken);

            _orderContext.Orders.Remove(orderDB);

            await _orderContext.SaveChangesAsync(cancellationToken);

            // Possibile invio fanout RabbitMQ per possibile rimborso ordine, ecc...
        }
    }
}
