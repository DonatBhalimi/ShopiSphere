using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetByAnnonIdAsync(string annonId);
        Task AddAsync(Order order);
        Task SaveChangesAsync();

    }
}
