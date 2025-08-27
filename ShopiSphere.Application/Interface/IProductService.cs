using ShopiSphere.Application.DTO;
using ShopiSphere.Application.DTOs;
using ShopiSphere.Domain.Entities;

namespace ShopiSphere.Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllAsync();
    Task<ProductDTO?> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
    Task DeleteAsync(Guid id);
}
