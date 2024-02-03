using OrdiniService.Models.API.Entity;

namespace OrdiniService.Models.API.Request
{
    public class CreateOrderRequest
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }
        public List<OrderProductsDTO> Products { get; set; }

        public CreateOrderRequest(long userId, DateTime dataOrdine, List<OrderProductsDTO> products)
        {
            UserId = userId;
            DataOrdine = dataOrdine;
            Products = products;
        }

        public static CreateOrderRequest CreateOrderRequestFactory(long userId, DateTime dataOrdine, List<OrderProductsDTO> products)
        {
            return new CreateOrderRequest(userId, dataOrdine, products);
        }
    }
}
