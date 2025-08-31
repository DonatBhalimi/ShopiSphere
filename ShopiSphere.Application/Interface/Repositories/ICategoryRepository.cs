using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task SaveChangesAsync();

        Task<bool> LinkProductAsync(Guid productId, Guid categoryId);
        Task<bool> UnlinkProductAsync(Guid productId, Guid categoryId);
        Task<IEnumerable<Category>> GetByProductAsync(Guid productId);
    }
}
