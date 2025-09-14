using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }
        public string FullName { get; set; } = default!;
        public string Line1 { get; set; } = default!;
        public string Line2 { get; set; } 
        public string City { get; set; } = default!;
        public string State { get; set; } =default!;
        public string PostalCode { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
