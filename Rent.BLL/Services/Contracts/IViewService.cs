using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using temp;

namespace Rent.BLL.Services.Contracts;

public interface IViewService
{
    Task<IEnumerable<VwCertificateForTenantToGetDto>> GetCertificateForTenant(Guid tenantId);

    Task<IEnumerable<VwRoomsWithTenantToGetDto>> GetRoomsWithTenants(DateTime dateTime);

    Task<IEnumerable<VwTenantAssetPaymentToGetDto>> GetTenantAssetPayment(Guid tenantId);
}