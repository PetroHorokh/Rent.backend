using Rent.DAL.DTO;
using Rent.DAL.Models;

namespace Rent.BLL.Services.Contracts;

public interface ITenantService
{
    Task<IEnumerable<TenantToGetDto>> GetAllTenantsAsync();

    Task<TenantToGetDto?> GetTenantByIdAsync(Guid tenantId);

    Task<TenantToGetDto?> GetTenantByNameAsync(string tenantName);

    Task<AddressToGetDto?> GetTenantAddressByTenantIdAsync(Guid tenantId);

    Task CreateTenant(TenantToCreateDto tenant);

    Task DeleteTenant(Guid tenantId);

    Task UpdateTenant(TenantToUpdateDto newTenant);
}