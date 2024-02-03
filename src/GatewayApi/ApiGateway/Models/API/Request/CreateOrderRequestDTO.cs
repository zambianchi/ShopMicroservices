using ApiGateway.Models.API.Entity;

namespace ApiGateway.Models.API.Request
{
    public class CreateOrderRequestDTO
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }
        public List<OrderProductsDTO> Products { get; set; }
    }
}
