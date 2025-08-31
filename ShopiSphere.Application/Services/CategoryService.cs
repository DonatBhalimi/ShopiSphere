
using ShopiSphere.Application.DTO;
using ShopiSphere.Application.Interface.Repositories;
using ShopiSphere.Application.Interface.Services;
using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categories;
        public CategoryService(ICategoryRepository categories)
        {
            _categories = categories;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
            => (await _categories.GetAllAsync()).Select(ToDto);
        public async Task<CategoryDTO?> GetByIdAsync(Guid id)
        {
            var c = await _categories.GetByIdAsync(id);
            return c is null ? null : ToDto(c);
        }
        public async Task AddAsync(Category category)
        {
            await _categories.AddAsync(category);
            await _categories.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            await _categories.UpdateAsync(category);
            await _categories.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var c = await _categories.GetByIdAsync(id);
            if (c == null) return;
            await _categories.DeleteAsync(c);
            await _categories.SaveChangesAsync();
        }
        public async Task<IEnumerable<CategoryDTO>> GetByProductAsync(Guid productId)
            => (await _categories.GetByProductAsync(productId)).Select(ToDto);
        public Task<bool> LinkProductAsync(Guid productId, Guid categoryId)
            => _categories.LinkProductAsync(productId, categoryId);
        public Task<bool> UnlinkProductAsync(Guid productId, Guid categoryId)
            => _categories.UnlinkProductAsync(productId, categoryId);

        private static CategoryDTO ToDto(Category c) => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            Description = c.Description
        };
    }
}
