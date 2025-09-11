using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string? AnnonId { get; set; }
        public Guid? UserId {  get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "USD";
        public IEnumerable<OrderItemDTO> Items { get; set; } = Enumerable.Empty<OrderItemDTO>();
    }
}
