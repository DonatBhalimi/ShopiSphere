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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository) {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task<OrderDTO> CreateFromCartAsync(string annonId)
        {
            var cart = await _cartRepository.GetByAnonIdAsync(annonId);
            if (cart is null || !cart.Items.Any()) throw new Exception("Cart is empty");

            var order = new Order
            {
                AnonId = cart.AnonId,
                UserId = cart.UserID,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                Currency = cart.Items.First().Currency
            };
            foreach(var c in cart.Items)
            {
                order.Items.Add(new OrderItem
                {
                    ProductVariantId = c.ProductVariantId,
                    Quantity = c.Quantity,
                    UnitPrice = c.UnitPrice,
                    Currency = c.Currency,
                    Sku = "",
                    Name = ""
                });
            }
            order.TotalAmount = order.Items.Sum(i=> i.Quantity* i.UnitPrice);
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            return ToDto(order);
        }
        public async Task<OrderDTO?> GeyByIdAsync(Guid id)
        {
            var o =  await _orderRepository.GetByIdAsync(id);
            return o is null ? null : ToDto(o); 
        }
        public async Task<IEnumerable<OrderDTO>> GetByAnnonIdAsync(string annonId)
            => (await _orderRepository.GetByAnnonIdAsync(annonId)).Select(ToDto);

        private static OrderDTO ToDto(Order o) => new OrderDTO
        {
            Id = o.Id,
            AnnonId = o.AnonId,
            UserId = o.UserId,
            CreatedAt = o.CreatedAt,
            Status = o.Status,
            TotalAmount = o.TotalAmount,
            Currency = o.Currency,
            Items = o.Items.Select(i => new OrderItemDTO
            {
                Id = i.Id,
                ProductVariantId = i.ProductVariantId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Currency = i.Currency,
                Sku = i.Sku,
                Name = i.Name,
            })
        };
    }
}
