using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetByAnonIdAsync(string anonId);
        Task<Cart?> GetByIdAsync(Guid id);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task SaveChangesAsync();

        Task<CartItem?> GetItemAsync(Guid itemId);
        Task AddItemAsync(CartItem item);
        Task UpdateItemAsync(CartItem item);
        Task RemoveItemAsync(CartItem item);
    }
}
