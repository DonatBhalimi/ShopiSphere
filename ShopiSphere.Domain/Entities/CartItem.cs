using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = default!;
        public Guid ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = "USD";
    }
}
