using AutoMapper;
using Microsoft.Extensions.Logging;
using Rent.BLL.Services.Contracts;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.UnitOfWork;

namespace Rent.BLL.Services;

public class TenantService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TenantService> logger) : ITenantService
{
    public async Task<IEnumerable<TenantToGetDto>> GetAllTenantsAsync()
    {
        logger.LogInformation("Entering TenantService, GetAllTenantsAsync");

        logger.LogInformation("Calling TenantRepository, method GetAllAsync");
        var tenants = (await unitOfWork.Tenants.GetAllAsync()).ToList();
        logger.LogInformation("Finished calling TenantRepository, method GetAllAsync");

        logger.LogInformation($"Mapping tenants to TenantToGetDto");
        var result = tenants.Select(mapper.Map<TenantToGetDto>);

        logger.LogInformation("Exiting TenantService, GetAllTenantsAsync");
        return result;
    }

    public async Task<TenantToGetDto?> GetTenantByIdAsync(Guid tenantId)
    {
        logger.LogInformation("Entering TenantService, GetTenantByIdAsync");

        logger.LogInformation("Calling TenantRepository, method GetSingleByConditionAsync");
        logger.LogInformation($"Parameter: tenantId = {tenantId}");
        var tenant = await unitOfWork.Tenants.GetSingleByConditionAsync(tenant => tenant.TenantId == tenantId);
        logger.LogInformation("Finished calling TenantRepository, method GetSingleByConditionAsync");

        logger.LogInformation($"Mapping tenant to TenantToGetDto");
        var result = mapper.Map<TenantToGetDto>(tenant);

        logger.LogInformation("Exiting TenantService, GetTenantByIdAsync");
        return result;
    }

    public async Task<TenantToGetDto?> GetTenantByNameAsync(string tenantName)
    {
        logger.LogInformation("Entering TenantService, GetTenantByNameAsync");

        logger.LogInformation("Calling TenantRepository, method GetSingleByConditionAsync");
        logger.LogInformation($"Parameter: TenantName = {tenantName}");
        var tenant = await unitOfWork.Tenants.GetSingleByConditionAsync(tenant => tenant.Name == tenantName);
        logger.LogInformation("Finished calling TenantRepository, method GetSingleByConditionAsync");

        logger.LogInformation($"Mapping tenant to TenantToGetDto");
        var result = mapper.Map<TenantToGetDto>(tenant);

        logger.LogInformation("Exiting TenantService, GetTenantByNameAsync");
        return result;
    }

    public async Task<AddressToGetDto?> GetTenantAddressByTenantIdAsync(Guid tenantId)
    {
        logger.LogInformation("Entering TenantService, GetTenantAddressByTenantIdAsync");

        logger.LogInformation("Calling TenantRepository, method GetSingleByConditionAsync");
        logger.LogInformation($"Parameter: TenantId = {tenantId}");
        var address = (await unitOfWork.Tenants.GetSingleByConditionAsync(tenant => tenant.TenantId == tenantId,
            tenant => tenant.Address!))!.Address;
        logger.LogInformation("Finished calling TenantRepository, method GetSingleByConditionAsync");

        logger.LogInformation($"Mapping tenant to TenantToGetDto");
        var result = mapper.Map<AddressToGetDto>(address);

        logger.LogInformation("Exiting TenantService, GetTenantByNameAsync");
        return result;
    }
    
    public async Task CreateTenant(TenantToCreateDto tenant)
    {
        logger.LogInformation("Entering TenantService, CreateTenant");

        logger.LogInformation("Calling TenantRepository, method CreateWithProcedure");
        logger.LogInformation($"Parameters: AddressId = {tenant.AddressId}, Name = {tenant.Name}, BankName = {tenant.BankName}, Director = {tenant.Director}, Description = {tenant.Description}");
        await unitOfWork.Tenants.CreateWithProcedure(tenant);
        logger.LogInformation("Finished calling TenantRepository, method CreateWithProcedure");

        await unitOfWork.SaveAsync();
        logger.LogInformation("Exiting TenantService, CreateTenant");
    }

    public async Task DeleteTenant(Guid tenantId)
    {
        logger.LogInformation("Entering TenantService, DeleteTenant");

        logger.LogInformation("Calling TenantRepository, method GetSingleByConditionAsync");
        logger.LogInformation($"Parameter: TenantId = {tenantId}");
        var tenant = await unitOfWork.Tenants.GetSingleByConditionAsync(tenant => tenant.TenantId == tenantId);
        logger.LogInformation("Finished calling TenantRepository, method GetSingleByConditionAsync");
        
        if (tenant != null)
        {
            logger.LogInformation("Calling TenantRepository, method Delete");
            unitOfWork.Tenants.Delete(tenant);
            logger.LogInformation("Finished calling TenantRepository, method Delete");
            await unitOfWork.SaveAsync();
        }
        
        logger.LogInformation("Entering TenantService, DeleteTenant");
    }

    public async Task UpdateTenant(TenantToUpdateDto newTenant)
    {
        logger.LogInformation("Entering TenantService, UpdateTenant");

        logger.LogInformation("Calling TenantRepository, method GetSingleByConditionAsync");
        logger.LogInformation($"Parameters: AddressId = {newTenant.AddressId}, Name = {newTenant.Name}, BankName = {newTenant.BankName}, Director = {newTenant.Director}, Description = {newTenant.Description}");
        var tenant =
            await unitOfWork.Tenants.GetSingleByConditionAsync(tenant => tenant.TenantId == newTenant.TenantId);
        logger.LogInformation("Finished calling TenantRepository, method GetSingleByConditionAsync");

        if (tenant != null)
        {
            tenant.Name = newTenant.Name;
            tenant.BankName = newTenant.BankName;
            tenant.AddressId = newTenant.AddressId;
            tenant.Director = newTenant.Director;
            tenant.Description = newTenant.Description;

            logger.LogInformation("Calling TenantRepository, method Update");
            unitOfWork.Tenants.Update(tenant);
            logger.LogInformation("Finished calling TenantRepository, method Update");

            await unitOfWork.SaveAsync();

            logger.LogInformation("Entering TenantService, UpdateTenant");
        }
    }
}