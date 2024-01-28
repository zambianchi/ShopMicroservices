using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdiniService.Models
{
    [Keyless]
    public class OrderProducts
    {
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        public long IdProduct { get; set; }
        public virtual Order Order { get; set; }
    }
}
