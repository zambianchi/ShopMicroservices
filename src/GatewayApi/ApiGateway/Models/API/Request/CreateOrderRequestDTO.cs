namespace ApiGateway.Models.API.Request
{
    public class CreateOrderRequestDTO
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }
        public List<long> ProductIds { get; set; }
    }
}
