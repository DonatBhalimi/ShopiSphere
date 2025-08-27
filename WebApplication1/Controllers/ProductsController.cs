using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopiSphere.Infrastructure.Persistance;

namespace ShopiSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _db.Product
                .Include(products => products.Variants)
                .ToListAsync();
            return Ok(products);
        }
    }
}
