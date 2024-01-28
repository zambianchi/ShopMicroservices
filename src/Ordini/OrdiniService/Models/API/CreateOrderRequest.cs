namespace OrdiniService.Models.API
{
    public class CreateOrderRequest
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }
    }
}
