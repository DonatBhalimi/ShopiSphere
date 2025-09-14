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
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _db;
        public AddressRepository (AppDbContext db) => _db = db;
        
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _db.Addresses.OrderBy(a => a.FullName).ToListAsync();
        }
        public Task<Address?> GetByIdAsync(Guid id)
        {
            return _db.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }
        public Task AddAsync(Address address)
        {
            _db.Addresses.Add(address);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(Address address)
        {
            _db.Addresses.Update(address);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Address address)
        {
            _db.Remove(address);
            return Task.CompletedTask;
        }
        public Task SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
