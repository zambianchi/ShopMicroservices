﻿using Microsoft.EntityFrameworkCore;
using OrdiniService.Models.DB;

namespace OrdiniService.Context
{
    public class OrderContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }

        public OrderContext() { }

        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
            // "Server=localhost,5433;Database=OrderDB;User Id=SA;Password=yourStrong(!)Password@word;Encrypt=True;TrustServerCertificate=True"
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => new { op.IdProduct, op.OrderId });

            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
