using ShopiSphere.Application.DTO;
using ShopiSphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopiSphere.Application.Interface.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDTO>> GetAllAsync();
        Task<AddressDTO?> GetByIdAsync(Guid id);
        Task AddAsync(Address address);
        Task UpdateAsync(Address address);
        Task DeleteAsync(Guid id);
    }
}
