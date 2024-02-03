using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrdiniService.Context;
using OrdiniService.Models.API.Entity;
using OrdiniService.Models.API.Request;
using OrdiniService.Models.DB;
using OrdiniService.Models.ExternalAPI.Entity;
using OrdiniService.Models.ExternalAPI.Request;
using OrdiniService.Services.Int;
using OrdiniService.SubServices.Int;

namespace OrdiniService.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _orderContext;
        private readonly IOrderSubService _orderSubService;

        public OrderService(OrderContext orderContext, IOrderSubService orderSubService)
        {
            this._orderContext = orderContext;
            this._orderSubService = orderSubService;
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
                    .ToList();

                List<OrderProductsDTO> orderProducts = new List<OrderProductsDTO>();
                foreach (var product in orderProducts)
                {
                    var orderProduct = OrderProductsDTO.OrderProductsDTOFactory(product.IdProduct, product.Amount);
                    orderProducts.Add(orderProduct);
                }

                orders.Add(OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, orderProducts));
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

            List<OrderProductsDTO> orderProducts = new List<OrderProductsDTO>();
            foreach (var product in orderDB.OrderProducts)
            {
                var orderProduct = OrderProductsDTO.OrderProductsDTOFactory(product.IdProduct, product.Amount);
                orderProducts.Add(orderProduct);
            }

            return OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, orderProducts);
        }

        /// <summary>
        /// Crea un ordine
        /// </summary>
        public async Task<OrderDTO> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request.Products.IsNullOrEmpty())
            {
                throw new Exception("Nessun articolo da associare al nuovo ordine");
            }

            var orderProductsRequest = request.Products
                .Select(x => new OrderProducts
                {
                    IdProduct = x.IdProduct,
                    Amount = x.Amount
                })
                .ToList();

            var orderDB = new Order
            {
                Date = request.DataOrdine,
                CreationAccountId = request.UserId,
                OrderProducts = orderProductsRequest
            };

            // Aggiungi il nuovo ordine al DB
            await _orderContext.Orders
                .AddAsync(orderDB, cancellationToken);

            await _orderContext.SaveChangesAsync(cancellationToken);

            // Riporto prodotti venduti
            var soldProductsForRequest = orderProductsRequest
                .Select(x => ProductAvailableForRequestApiDTO.ProductAvailableForRequestApiDTOFactory(x.IdProduct, x.Amount))
                .ToList();

            var reportSoldProductsRequest = EditProductsAvailableAmountRequestApiDTO.EditProductsAvailableAmountRequestApiDTOFactory(soldProductsForRequest);
            await this._orderSubService.ReportSoldProducts(reportSoldProductsRequest, cancellationToken);

            return OrderDTO.OrderDTOFactory(orderDB.Id, orderDB.Date, orderDB.CreationAccountId, request.Products);
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
