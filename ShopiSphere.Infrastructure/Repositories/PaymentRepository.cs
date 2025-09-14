using Microsoft.EntityFrameworkCore;
using ShopiSphere.Application.Interface.Repositories;
using ShopiSphere.Domain.Entities;
using ShopiSphere.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Infrastructure.Repositories
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly AppDbContext _appDbContext;
        public PaymentRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;
        public Task AddAsync(Payment p)
        {
            _appDbContext.Add(p);
            return Task.CompletedTask;
        }
        public Task SaveAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }
        public Task<Order?> LoadOrderAsync(Guid id)
        {
            return _appDbContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        }
        public Task<Payment?> GetByProviderIdAsync(string pid)
        {
            return _appDbContext.Payments.FirstOrDefaultAsync(p => p.ProviderPaymentIntentId == pid);
        }

    }
}
