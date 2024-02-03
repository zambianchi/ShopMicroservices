using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdiniService.Models.DB
{
    [Keyless]
    public class OrderProducts
    {
        [ForeignKey("Order")]
        public long OrderId { get; set; }
        public long IdProduct { get; set; }
        public int Amount { get; set; }
        public virtual Order Order { get; set; }
    }
}
