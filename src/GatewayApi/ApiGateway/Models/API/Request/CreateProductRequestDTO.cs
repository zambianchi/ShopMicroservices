using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Models.API.Request
{
    public class CreateProductRequestDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public int AvailableAmount { get; set; }
    }
}
