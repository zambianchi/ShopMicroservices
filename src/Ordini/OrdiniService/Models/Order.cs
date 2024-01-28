using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

namespace OrdiniService.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long CreationAccountId { get; set; }
        public long DeliveryAddressId { get; set; }
        public virtual List<OrderProducts> OrderProducts { get; set; }
    }
}
