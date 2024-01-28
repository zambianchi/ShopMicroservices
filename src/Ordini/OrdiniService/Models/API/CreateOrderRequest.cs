namespace OrdiniService.Models.API
{
    public class CreateOrderRequest
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }
        public List<long> ProductIds { get; set; }

        public CreateOrderRequest(long userId, DateTime dataOrdine, List<long> productIds)
        {
            this.UserId = userId;
            this.DataOrdine = dataOrdine;
            this.ProductIds = productIds;
        }

        public static CreateOrderRequest CreateOrderRequestFactory(long userId, DateTime dataOrdine, List<long> productIds)
        {
            return new CreateOrderRequest(userId, dataOrdine, productIds);
        }
    }
}
