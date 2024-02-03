using ApiGateway.Models.API.Entity;
using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Models.API.Request
{
    public class CreateOrderRequestDTO
    {
        public long UserId { get; set; } // Sarebbe preso da auth JWT
        public DateTime DataOrdine { get; set; }
        [Required]
        public List<OrderProductsDTO> Products { get; set; }
    }
}
