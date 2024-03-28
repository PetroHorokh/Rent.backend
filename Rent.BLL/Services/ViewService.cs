using AutoMapper;
using Rent.BLL.Services.Contracts;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.UnitOfWork;

namespace Rent.BLL.Services;

public class ViewService(IUnitOfWork unitOfWork, IMapper mapper) : IViewService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<VwCertificateForTenantToGetDto>> GetCertificateForTenant(Guid tenantId)
    {
        var entities = (await _unitOfWork
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
        return entities;
    }

    public async Task<IEnumerable<VwRoomsWithTenantToGetDto>> GetRoomsWithTenants(DateTime dateTime)
    {
        var entities = await _unitOfWork.Views.GetRoomsWithTenants(dateTime);

        return entities.Select(entity => _mapper.Map<VwRoomsWithTenantToGetDto>(entity));
    }

    public async Task<IEnumerable<VwTenantAssetPaymentToGetDto>> GetTenantAssetPayment(Guid tenantId)
    {
        var entities = await _unitOfWork.Views.GetTenantAssetPayment(tenantId);
         
        return entities.Select(entity => _mapper.Map<VwTenantAssetPaymentToGetDto>(entity));
    }
}