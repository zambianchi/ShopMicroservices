namespace OrdiniService.Models.API.Request
{
    public class CreateOrderRequest
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }
        public List<long> ProductIds { get; set; }

        public CreateOrderRequest(long userId, DateTime dataOrdine, List<long> productIds)
        {
            UserId = userId;
            DataOrdine = dataOrdine;
            ProductIds = productIds;
        }

        public static CreateOrderRequest CreateOrderRequestFactory(long userId, DateTime dataOrdine, List<long> productIds)
        {
            return new CreateOrderRequest(userId, dataOrdine, productIds);
        }
    }
}
