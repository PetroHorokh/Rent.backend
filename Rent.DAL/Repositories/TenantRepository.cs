using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;
using System.Net;

namespace Rent.DAL.Repositories;

public class TenantRepository(RentContext context) : RepositoryBase<Tenant>(context), ITenantRepository
{
    public async Task CreateWithProcedure(TenantToCreateDto tenant)
    {
        await context.Database.ExecuteSqlAsync(
            $"EXEC [dbo].[sp_Tenant_Insert] @AddressId = '{tenant.AddressId}', @Name = '{tenant.Name}', @BankName = '{tenant.BankName}', @Director = '{tenant.Director}', @Description = '{tenant.Description}'");
    }
}