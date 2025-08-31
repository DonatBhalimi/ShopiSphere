using ShopiSphere.Application.DTO;
using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(Guid id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<CategoryDTO>> GetByProductAsync(Guid productId);
        Task<bool> LinkProductAsync(Guid productId, Guid categoryId);
        Task<bool> UnlinkProductAsync(Guid productId, Guid categoryId);
    }
}
