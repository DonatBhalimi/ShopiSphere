using Microsoft.AspNetCore.Mvc;
using ShopiSphere.Application.Interface.Services;
using ShopiSphere.Domain.Entities;


namespace ShopiSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : Controller
    {
        private readonly IAddressService _addressService;
        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _addressService.GetAllAsync());
        }

        [HttpGet("{id:guid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _addressService.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        public record CreateAddressRequest(string FullName, string Line1, string Line2,
            string City, string State, string PostalCode, string Country, Guid? UserId);

        public async Task<IActionResult> Create([FromBody] CreateAddressRequest request)
        {
            var a = new Address
            {
                FullName = request.FullName,
                Line1 = request.Line1,
                Line2 = request.Line2,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country,
                UserId = request.UserId
            };
            await _addressService.AddAsync(a);
            return CreatedAtAction(nameof(GetById), new { id = a.Id }, a);
        }

        public record UpdateAddressRequest(string FullName, string Line1, string Line2,
            string City, string State, string PostalCode, string Country, Guid? UserId);

        public async Task<IActionResult> Update(Guid id,[FromBody] UpdateAddressRequest request)
        {
            var existing = await _addressService.GetByIdAsync(id);
            if (existing == null) NotFound();

            var a = new Address
            {
                Id = id,
                FullName = request.FullName,
                Line1 = request.Line1,
                Line2 = request.Line2,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country,
                UserId = request.UserId
            };
            await _addressService.UpdateAsync(a);
            return NoContent(); 
        }
        [HttpDelete("{id:guid")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _addressService.DeleteAsync(id);
            return NoContent();
        }
    }
}
