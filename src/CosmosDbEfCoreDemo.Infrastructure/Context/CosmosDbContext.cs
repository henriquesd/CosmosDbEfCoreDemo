using CosmosDbEfCoreDemo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosDbEfCoreDemo.Infrastructure.Context
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAutoscaleThroughput(1000);

            modelBuilder.HasDefaultContainer("Products");

            modelBuilder.Entity<Product>()
                .HasNoDiscriminator()
                .HasPartitionKey(x => x.Category)
                .HasKey(x => x.ProductId);

            modelBuilder.Entity<Supplier>()
                .HasNoDiscriminator()
                .ToContainer("Suppliers")
                .HasPartitionKey(x => x.ProductId)
                .HasKey(x => x.SupplierId);

            modelBuilder.Entity<Inventory>()
                .HasNoDiscriminator()
                .ToContainer("Inventory")
                .HasPartitionKey(x => x.ProductId)
                .HasKey(x => x.InventoryId);
        }
    }
}