using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentIntentAsync(Guid orderId);
        Task HandleStripeWebhookAsync(string payload, String signatureHeader);
    }
}
