using Microsoft.EntityFrameworkCore;
using ProdottiService.Models;

namespace ProdottiService.Context
{
    public class ProductsContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public ProductsContext() { }

        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
            // "Server=localhost,5434;Database=ProductDB;User Id=SA;Password=yourStrong(!)Password@word;Encrypt=True;TrustServerCertificate=True"
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost,5434;Database=ProductDB;User Id=SA;Password=yourStrong(!)Password@word;Encrypt=True;TrustServerCertificate=True");
        //    }
        //}
    }
}
