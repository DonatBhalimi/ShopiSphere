
using Microsoft.AspNetCore.Mvc;
using ShopiSphere.Application.Interface.Services;
using ShopiSphere.Domain.Entities;

namespace ShopiSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("id:{guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }
        public record CreateCategoryRequest(string Name,string Slug,string? description);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest req)
        {
            var category = new Category { Name = req.Name, Slug = req.Slug, Description = req.description };
            await _categoryService.AddAsync(category);
            return CreatedAtAction(nameof(GetByIdAsync), new {id= category.Id},category);
        } 
        public record UpdateCategoryRequest(string Name,string Slug,string? description);

        [HttpPut("{id:guid}")]
        public async Task<IActionResult>Update(Guid id, [FromBody] UpdateCategoryRequest req)
        {
            var existing = await _categoryService.GetByIdAsync(id);
            if (existing is null) return NotFound();
            var category = new Category { Id = id, Name = req.Name, Slug = req.Slug, Description = req.description };
            await _categoryService.UpdateAsync(category);
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
