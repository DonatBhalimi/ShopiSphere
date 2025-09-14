using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment p);
        Task SaveAsync();
        Task<Order?> LoadOrderAsync(Guid id);
        Task<Payment?> GetByProviderIdAsync(string id);
    }
}
