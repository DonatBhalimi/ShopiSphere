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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return product.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                Description = p.Description,
                MinPrice = p.Variants.Any() ? p.Variants.Min(v => v.Price) : 0,
                MaxPrice = p.Variants.Any() ? p.Variants.Max(v => v.Price) : 0,
            });

        }
        public async Task<ProductDTO?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                MinPrice = product.Variants.Any() ? product.Variants.Min(v => v.Price) : 0,
                MaxPrice = product.Variants.Any() ? product.Variants.Max(v => v.Price) : 0
            };
        }

        public async Task AddAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return;
            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}
