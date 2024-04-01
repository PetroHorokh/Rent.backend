using AutoMapper;
using Microsoft.Extensions.Logging;
using Rent.BLL.Services.Contracts;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.UnitOfWork;

namespace Rent.BLL.Services;

public class ViewService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ViewService> logger) : IViewService
{
    public async Task<IEnumerable<VwCertificateForTenantToGetDto>> GetCertificateForTenant(Guid tenantId)
    {
        logger.LogInformation("Entering ViewService, GetCertificateForTenant");

        logger.LogInformation("Calling ViewRepository, method GetCertificateForTenant");
        var entities = (await unitOfWork
            .Views
            .GetCertificateForTenant(tenantId))
            .GroupBy(entity => entity.RentId)
            .Select(group => new VwCertificateForTenantToGetDto
            {
                RentId = group.Key,
                RentStartDate = group.Min(entity => entity.RentStartDate),
                RentEndDate = group.Max(entity => entity.RentStartDate),
                BillIds = string.Join(",\n", group.Select(obj => obj.BillId)),
                PaymentIds = string.Join(",\n", group.Where(obj => obj.PaymentId != null).Select(obj => obj.PaymentId)),
            }); ;
        logger.LogInformation("Finished calling ViewRepository, method GetCertificateForTenant");

        logger.LogInformation("Exiting ViewService, GetCertificateForTenant");
        return entities;
    }

    public async Task<IEnumerable<VwRoomsWithTenantToGetDto>> GetRoomsWithTenants(DateTime dateTime)
    {
        logger.LogInformation("Entering ViewService, GetRoomsWithTenants");

        logger.LogInformation("Calling ViewRepository, method GetRoomsWithTenants");
        var entities = await unitOfWork.Views.GetRoomsWithTenants(dateTime);
        logger.LogInformation("Finished calling ViewRepository, method GetRoomsWithTenants");

        logger.LogInformation("Exiting ViewService, GetRoomsWithTenants");
        return entities.Select(mapper.Map<VwRoomsWithTenantToGetDto>);
    }

    public async Task<IEnumerable<VwTenantAssetPaymentToGetDto>> GetTenantAssetPayment(Guid tenantId)
    {
        logger.LogInformation("Entering ViewService, GetTenantAssetPayment");

        logger.LogInformation("Calling ViewRepository, method GetTenantAssetPayment");
        var entities = await unitOfWork.Views.GetTenantAssetPayment(tenantId);
        logger.LogInformation("Finished calling ViewRepository, method GetTenantAssetPayment");

        logger.LogInformation("Exiting ViewService, GetTenantAssetPayment");
        return entities.Select(mapper.Map<VwTenantAssetPaymentToGetDto>);
    }
}