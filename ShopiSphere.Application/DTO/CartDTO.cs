using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.DTO
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public string? AnonId { get; set; }
        public Guid? UserId { get; set; }
        public IEnumerable<CartItemDTO> Items { get; set; } = Enumerable.Empty<CartItemDTO>();
    }
}
