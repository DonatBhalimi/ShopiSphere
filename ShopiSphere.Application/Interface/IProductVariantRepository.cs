using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface
{
    public interface IProductVariantRepository
    {
        Task <IEnumerable<ProductVariant>> GetByProductIdAsync(Guid productId);
        Task<ProductVariant?> GetByIdAsync(Guid id);
        Task AddAsync(ProductVariant variant);
        Task UpdateAsync(ProductVariant variant);
        Task DeleteAsync(ProductVariant variant);
        Task SaveChangesAsync();
    }
}
