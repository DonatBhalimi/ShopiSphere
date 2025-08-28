using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.DTO
{
    public class ProductVariantDTO
    {
        public Guid Id { get; set; }
        public string StockKeepingUnit { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "USD";
        public int InventoryQuantity { get; set; }
    }
}
