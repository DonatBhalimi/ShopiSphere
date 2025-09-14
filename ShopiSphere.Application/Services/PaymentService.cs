using ShopiSphere.Application.Interface.Repositories;
using ShopiSphere.Application.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
namespace ShopiSphere.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly string _secretKey;
        private readonly string _webhookSecret;
        
        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IConfiguration cfg)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _secretKey = cfg["Stripe:SecretKey"];
            _webhookSecret = cfg["Stripe: WebhookSecret"];
            StripeConfiguration.ApiKey = _secretKey;
        }

        public async Task<string> CreatePaymentIntentAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId) ?? throw new KeyNotFoundException("Order not found");
            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = (long)(order.TotalAmount * 100m),
                Currency = order.Currency.ToLower(),
                Metadata = new Dictionary<string, string> { { "orderId", order.Id.ToString() } }
            });
            await _paymentRepository.AddAsync(new Domain.Entities.Payment
            {
                OrderId = orderId,
                Amount = order.TotalAmount,
                Currency = order.Currency,
                ProviderPaymentIntentId = intent.Id,
                Status = "Created"
            });
            await _paymentRepository.SaveAsync();
            return intent.ClientSecret;
        }
        public async Task HandleStripeWebhookAsync(string payload, string signatureHeader)
        {
            var ev = EventUtility.ConstructEvent(payload, signatureHeader, _webhookSecret);
            if (ev.Type == "payment_intent.succeeded")
            {
                var pi = (PaymentIntent)ev.Data.Object;
                var p = await _paymentRepository.GetByProviderIdAsync(pi.Id);
                if (p != null)
                {
                    p.Status = "Succeeded";
                    var order = await _orderRepository.GetByIdAsync(p.Id);
                    if (order != null)
                    {
                        order.Status = "Paid";
                        await _paymentRepository.SaveAsync();
                        await _orderRepository.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
