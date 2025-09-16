using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Domain.Entities
{
    public class Base
    {
        [Key]
        public Guid Id { get; set; }
    }
}
