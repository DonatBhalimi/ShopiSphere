using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopiSphere.Application.Interfaces.Services;
using ShopiSphere.Domain.Entities;
using ShopiSphere.Infrastructure.Persistance;

namespace ShopiSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productService.GetAllAsync();
            return Ok(product);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var dto = await _productService.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }
        public record CreateProductRequest(string Name, string Slug, string? Description);
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest req)
        {
            var product = new Product { Name = req.Name, Slug = req.Slug, Description = req.Description };
            await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id}, product);
        }
        public record UpdateProductRequest(string Name,string Slug,string? Description);
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest req)
        {
            var existing = await _productService.GetByIdAsync(id);
                if (existing == null)
            {
                return NotFound();
            }
            var product = new Product
            {
                Id = id,
                Name = req.Name,
                Slug = req.Slug,
                Description = req.Description
            };
            await _productService.UpdateAsync(product);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult>Delete (Guid id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
