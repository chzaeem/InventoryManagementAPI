using InventoryManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InventoryManagement.API.Database
{
    public class DatabaseContext : IdentityDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Product", "dbo");
            modelBuilder.Entity<Sale>().ToTable("Sale", "dbo");
            modelBuilder.Entity<Purchase>().ToTable("Purchase", "dbo");

            modelBuilder.Entity<Sale>()
                .HasOne(p => p.Product)
                .WithOne()
                .HasForeignKey<Product>(ad => ad.Id);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Product)
                .WithOne()
                .HasForeignKey<Product>(ad => ad.Id);
        }

    }
}
