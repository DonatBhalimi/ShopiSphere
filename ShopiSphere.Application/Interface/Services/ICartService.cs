using ShopiSphere.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetOrCreateByAnonIdAsync(string anonId);
        Task<CartDTO> AddItemAsync(string anonId, Guid productVariantId, int quantity);
        Task<bool> UpdateItemQtyAsync(Guid cartItemId, int quantity);
        Task<bool> RemoveItemAsync(Guid cartItemId);
    }
}
