using ShopiSphere.Application.DTO;
using ShopiSphere.Application.Interface;
using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductRepository _products;
        private readonly IProductVariantRepository _variant;

        public ProductVariantService(IProductRepository products, IProductVariantRepository variant)
        {
            _products = products;
            _variant = variant;
        }
        public async Task<IEnumerable<ProductVariantDTO>> GetByProductAsync(Guid productId)
        {
            var list = await _variant.GetByProductIdAsync(productId);
            return list.Select(v => new ProductVariantDTO
            {
                Id = v.Id,
                StockKeepingUnit = v.StockKeepingUnit,
                Price = v.Price,
                Currency = v.Currency,
                InventoryQuantity = v.InventoryQuantity,
            });
        }
        public async Task<Guid> AddAsync(Guid productId, string StockKeepingUnit, decimal price, string currency, int inventoryQuantity)
        {
            var product = _products.GetByIdAsync(productId);
            if (product == null) throw new KeyNotFoundException("Product not found");

            var v = new ProductVariant
            {
                ProductId = productId,
                StockKeepingUnit = StockKeepingUnit,
                Price = price,
                Currency = currency,
                InventoryQuantity = inventoryQuantity
            };
            await _variant.AddAsync(v);
            await _variant.SaveChangesAsync();
            return v.Id;
        }
    }
}
