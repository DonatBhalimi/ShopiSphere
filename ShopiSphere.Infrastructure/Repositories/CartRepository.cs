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
    public class CartRepository: ICartRepository
    {
        private readonly AppDbContext _db;
        public CartRepository(AppDbContext db) => _db = db;

        public async Task<Cart?> GetByAnonIdAsync(string anonId)
            => await _db.Carts
            .Include(c=> c.Items)
            .FirstOrDefaultAsync(c => c.AnonId == anonId); 
        public Task<Cart?> GetByIdAsync(Guid id) 
            => _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);   
        public Task AddAsync(Cart cart)
        {
            _db.Carts.Add(cart);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(Cart cart)
        {
            _db.Carts.Update(cart);
            return Task.CompletedTask;
        }
        public Task SaveChangesAsync()=> _db.SaveChangesAsync();

        public Task<CartItem?> GetItemAsync(Guid itemId)
            => _db.CartItems.FirstOrDefaultAsync(i => i.Id == itemId);
        public Task AddItemAsync(CartItem item)
        {
            _db.CartItems.Add(item);
            return Task.CompletedTask;
        }
        public Task UpdateItemAsync(CartItem item)
        {
            _db.CartItems.Update(item);
            return Task.CompletedTask;
        }
        public Task RemoveItemAsync(CartItem item)
        {
            _db.CartItems.Remove(item);
            return Task.CompletedTask;
        }
    }
}
