using ShopiSphere.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateFromCartAsync(string annonId);
        Task<OrderDTO?> GeyByIdAsync(Guid id);
        Task<IEnumerable<OrderDTO>> GetByAnnonIdAsync(string annonId);
    }
}
