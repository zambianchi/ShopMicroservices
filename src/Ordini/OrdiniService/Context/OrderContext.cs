using Microsoft.EntityFrameworkCore;
using OrdiniService.Models;

namespace OrdiniService.Context
{
    public class OrderContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProducts> OrderProducts { get; set; }

        public OrderContext() { }

        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
            // "Server=localhost,5433;Database=OrderDB;User Id=SA;Password=yourStrong(!)Password@word;Encrypt=True;TrustServerCertificate=True"
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost,5433;Database=OrderDB;User Id=SA;Password=yourStrong(!)Password@word;Encrypt=True;TrustServerCertificate=True");
        //    }
        //}
    }
}
