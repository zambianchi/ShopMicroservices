using System.ComponentModel.DataAnnotations;

namespace OrdiniService.Models
{
    public class OrderProducts
    {
        [Key]
        public long IdLink { get; set; }
        public Order Order { get; set; }
        public List<long> IdProduct { get; set; }
    }
}
