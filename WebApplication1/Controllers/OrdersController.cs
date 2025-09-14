using Microsoft.AspNetCore.Mvc;
using ShopiSphere.Application.Interface.Services;

namespace ShopiSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpPost("from-cart")]
        public async Task<IActionResult> CreateFromCart([FromQuery] string anonId)
        {
            var order = await _orderService.CreateFromCartAsync(anonId);
            return CreatedAtAction(nameof(GetById), new {id = order.Id},order);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _orderService.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetByAnonId([FromQuery] string anonId)=> Ok (await _orderService.GetByAnnonIdAsync(anonId));   
    }
}
