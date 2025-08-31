using ShopiSphere.Application.DTO;
using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Services
{
    public interface IProductVariantService
    {
        Task<IEnumerable<ProductVariantDTO>> GetByProductAsync(Guid productId);
        Task<Guid> AddAsync(Guid productId, string StockKeepingUnit, decimal price, string currency, int inventoryQuantity);

    }
}
