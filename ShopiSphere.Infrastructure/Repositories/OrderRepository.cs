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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }
        public Task<Order?> GetByIdAsync(Guid id) => _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);
        public async Task<IEnumerable<Order>> GetByAnnonIdAsync(string annonId) 
            => await _db.Orders
            .Include(o => o.Items)
            .Where(o => o.AnonId == annonId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
        public Task AddAsync(Order order)
        {
            _db.Orders.Add(order);
            return Task.CompletedTask;
        }
        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
