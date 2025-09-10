using Microsoft.EntityFrameworkCore;
using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Infrastructure.Persistance
{
    public class AppDbContext: DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Product => Set<Product>();
        public DbSet<ProductVariant> ProductVariant => Set<ProductVariant>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasIndex(p => p.Slug)
            .IsUnique();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne(p => p.Product)
                .HasForeignKey(v => v.ProductId);

            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(v => v.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Cart>()
                .HasMany(c=> c.Items)
                .WithOne(i => i.Cart)
                .HasForeignKey(i=> i.CartId);

            modelBuilder.Entity<CartItem>()
                .Property(i => i.Quantity)
                .HasDefaultValue(1);
            
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);

            modelBuilder.Entity<OrderItem>()
                .Property (i => i.Quantity)
                .HasDefaultValue(1);
        }
    }
}
