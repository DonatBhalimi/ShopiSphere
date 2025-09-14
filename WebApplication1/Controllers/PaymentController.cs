using Microsoft.AspNetCore.Mvc;
using ShopiSphere.Application.Interface.Services;

namespace ShopiSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService) => _paymentService = paymentService;

        [HttpPost("create-intent/{orderId:guid}")]
        public async Task<IActionResult> CreateIntent(Guid orderId)
        {
            var clientSecret = await _paymentService.CreatePaymentIntentAsync(orderId);
            return Ok(new {clientSecret});  
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            using var reader = new StreamReader(Request.Body);
            var payload = await reader.ReadToEndAsync();
            var sig = Request.Headers["Stripe-Signature"];
            await _paymentService.HandleStripeWebhookAsync(payload, sig);
            return Ok();
        }
    }
}
