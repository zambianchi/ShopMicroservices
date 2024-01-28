namespace OrdiniService.Models.API
{
    public class CreateOrderRequest
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }

        public CreateOrderRequest(long userId, DateTime dataOrdine)
        {
            this.UserId = userId;
            this.DataOrdine = dataOrdine;
        }

        public static CreateOrderRequest CreateOrderRequestFactory(long userId, DateTime dataOrdine)
        {
            return new CreateOrderRequest(userId, dataOrdine);
        }
    }
}
