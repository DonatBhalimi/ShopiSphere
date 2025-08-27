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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Slug)
                .IsUnique();

            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Variants)
                .WithOne(p => p.Product)
                .HasForeignKey(v => v.ProductId);
        }
    }
}
