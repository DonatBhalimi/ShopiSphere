using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Domain.Entities
{
    public class ProductVariant
    {
        public Guid Id {  get; set; }= Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string StockKeepingUnit { get; set; } = default;
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public int InventoryQuantity { get; set; }
        public byte[] RowVersion { get; set; }= Array.Empty<byte>();

    }
}
