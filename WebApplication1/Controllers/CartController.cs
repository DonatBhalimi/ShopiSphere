using Microsoft.AspNetCore.Mvc;
using ShopiSphere.Application.Interface.Services;

namespace ShopiSphere.API.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) => _cartService = cartService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string anonId)
            => Ok(await _cartService.GetOrCreateByAnonIdAsync(anonId));

        public record AddItemRequest(string AnonId,Guid ProductVariantId, int Quantity);
        public record UpdateQtyRequest(int Quantity);

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddItemRequest request) 
            => Ok(await _cartService.AddItemAsync(request.AnonId,request.ProductVariantId,request.Quantity));
        [HttpPut("items/{itemId:guid}")]
        public async Task<IActionResult> UpdateQty(Guid itemId, [FromBody]  UpdateQtyRequest request)
            => Ok(await _cartService.UpdateItemQtyAsync(itemId,request.Quantity) ? NoContent() : NotFound());   
    }
}
