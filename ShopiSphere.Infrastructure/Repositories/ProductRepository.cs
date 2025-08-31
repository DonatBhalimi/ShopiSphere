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
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()=>
            await _appDbContext.Product.Include(p => p.Variants).ToListAsync();

        public Task<Product?> GetByIdAsync(Guid id) =>
            _appDbContext.Product.Include(p => p.Variants).FirstOrDefaultAsync(p => p.Id == id);

        public Task AddAsync(Product product)
        {
            _appDbContext.Add(product);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(Product product)
        {
            _appDbContext.Update(product);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Product product)
        {
            _appDbContext.Remove(product);
            return Task.CompletedTask;
        }
        public Task SaveChangesAsync() => _appDbContext.SaveChangesAsync();
    }
}
