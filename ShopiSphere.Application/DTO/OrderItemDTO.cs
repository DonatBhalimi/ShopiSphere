using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.DTO
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = "USD";
    }

}
