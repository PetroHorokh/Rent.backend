using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class RentRepository(RentContext context) : RepositoryBase<Rent>(context), IRentRepository
{
    public async Task CreateWithProcedure(RentToCreateDto rent)
    {
        await context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Rent_Insert] @AssetId = '{rent.AssetId}', @TenantId = '{rent.TenantId}', @StartDate = '{rent.StartDate}', @EndDate = '{rent.EndDate}'");
    }
}