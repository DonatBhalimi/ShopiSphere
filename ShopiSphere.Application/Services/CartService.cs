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
    public class CartService : ICartService
    {
        private readonly ICartRepository _carts;
        private readonly IProductVariantRepository _productVariantRepository;
        public CartService(ICartRepository cartRepository, IProductVariantRepository productVariantRepository)
        {
            _carts = cartRepository;
            _productVariantRepository = productVariantRepository;
        }
        public async Task<CartDTO> GetOrCreateByAnonIdAsync(string anonId)
        {
            var cart = await _carts.GetByAnonIdAsync(anonId);
            if (cart is null)
            {
                cart = new Domain.Entities.Cart { AnonId = anonId };
                await _carts.AddAsync(cart);
                await _carts.SaveChangesAsync();
            }
            return ToDto(cart);
        }
        public async Task<CartDTO> AddItemAsync(string anonId, Guid productVariantId, int quantity)
        {
            if (quantity <= 0) quantity = 1;
            var cart = await _carts.GetByAnonIdAsync(anonId);
            if (cart is null)
            {
                cart = new Cart { AnonId = anonId };
                await _carts.AddAsync(cart);
                await _carts.SaveChangesAsync();
            }
            var variant = await _productVariantRepository.GetByIdAsync(productVariantId);
            if (variant is null) throw new ArgumentException("Variant not found");

            var existing = cart.Items.FirstOrDefault(i => i.ProductVariantId == productVariantId);
            if (existing is null)
            {
                cart.Items.Add(new CartItem
                {
                    ProductVariantId = productVariantId,
                    Quantity = quantity,
                    UnitPrice = variant.Price,
                    Currency = variant.Currency,
                });
            }
            else
            {
                existing.Quantity += quantity;
            }
            cart.UpdateAt = DateTime.UtcNow;    
            await _carts.UpdateAsync(cart);
            await _carts.SaveChangesAsync();
            return ToDto(cart);
        }
        public async Task<bool> UpdateItemQtyAsync(Guid cartItemId, int quantity)
        {
            if (quantity <= 0) quantity = 1;
            var item = await _carts.GetItemAsync(cartItemId);
            if (item is null) return false;
            item.Quantity = quantity;
            await _carts.UpdateItemAsync(item);
            await _carts.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveItemAsync(Guid cartItemId)
        {
            var item = await _carts.GetItemAsync(cartItemId);
            if (item is null) return false;

            await _carts.RemoveItemAsync(item);
            await _carts.SaveChangesAsync();
            return true;
        }
        private static CartDTO ToDto(Cart c) => new CartDTO
        {
            Id = c.Id,
            AnonId = c.AnonId,
            UserId = c.UserID,
            Items = c.Items.Select(i => new CartItemDTO
            {
                Id = i.Id,
                ProductVariantId = i.ProductVariantId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Currency = i.Currency,
            })
        };
    }
}
