using Microsoft.EntityFrameworkCore;
using ShopiSphere.Application.Interface.Repositories;
using ShopiSphere.Domain.Entities;
using ShopiSphere.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Infrastructure.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _db;
        public ProductVariantRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ProductVariant>> GetByProductIdAsync(Guid productId)
        {
            return await _db.ProductVariant.Where(v => v.ProductId == productId).ToListAsync();
        }
        public Task<ProductVariant?> GetByIdAsync(Guid id)
        {
            return _db.ProductVariant.FirstOrDefaultAsync(v => v.Id == id);
        }
        public Task AddAsync(ProductVariant variant)
        {
            _db.ProductVariant.Add(variant);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(ProductVariant variant)
        {
            _db.ProductVariant.Update(variant);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(ProductVariant variant)
        {
            _db.ProductVariant.Remove(variant);
            return Task.CompletedTask;
        }
        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
