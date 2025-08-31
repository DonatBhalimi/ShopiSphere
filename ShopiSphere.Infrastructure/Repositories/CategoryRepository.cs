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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
            => await  _db.Categories.OrderBy(c => c.Name).ToListAsync();
        public async Task<Category?> GetByIdAsync(Guid id)
            => await _db.Categories.FirstOrDefaultAsync(c=> c.Id == id);

        public Task AddAsync(Category category)
        {
            _db.Categories.Add(category);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(Category category)
        {
            _db.Categories.Update(category);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Category category)
        {
            _db.Categories.Remove(category);
            return Task.CompletedTask;
        }
        public Task SaveChangesAsync() => _db.SaveChangesAsync();

        public async Task<bool> LinkProductAsync(Guid productId, Guid categoryId)
        {
            var exists = await _db.ProductCategories.AnyAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
            if (exists) return false;
            _db.ProductCategories.Add(new ProductCategory { ProductId= productId, CategoryId = categoryId });
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UnlinkProductAsync(Guid productId, Guid categoryId)
        {
            var link = await _db.ProductCategories
                .FirstOrDefaultAsync(pc =>pc.ProductId == productId && pc.CategoryId == categoryId);
            if (link == null) return false;

            _db.ProductCategories.Remove(link);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Category>> GetByProductAsync(Guid productId)
        {
            return await _db.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .Select(pc => pc.Category)
                .ToListAsync();

        }
    }
}
