using ShopiSphere.Application.DTO;
using ShopiSphere.Application.Interface.Repositories;
using ShopiSphere.Application.Interface.Services;
using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService (IAddressRepository addressRepository) => _addressRepository = addressRepository;

        public async Task<IEnumerable<AddressDTO>> GetAllAsync() => (await _addressRepository.GetAllAsync()).Select(ToDto);
        
        public async Task<AddressDTO?> GetByIdAsync(Guid id)
        {
            var a= await _addressRepository.GetByIdAsync(id);
            return a is null ? null : ToDto(a);
        }
        
        public async Task AddAsync(Address address)
        {
            await _addressRepository.AddAsync(address);
            await _addressRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Address address)
        {
            await _addressRepository.UpdateAsync(address);
            await _addressRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var a = await _addressRepository.GetByIdAsync(id);
            if (a is null)
            {
                return;
            }
            await _addressRepository.DeleteAsync(a);
            await _addressRepository.SaveChangesAsync();
        }
        private static AddressDTO ToDto(Address address) => new AddressDTO
        {
            Id = address.Id,
            UserId = address.UserId,
            FullName = address.FullName,
            Line1 = address.Line1,
            Line2 = address.Line2,
            City = address.City,
            State = address.State,
            PostalCode = address.PostalCode,
            Country = address.Country
        };
    }
}
