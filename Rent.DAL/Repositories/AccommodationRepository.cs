using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class AccommodationRepository(RentContext context) : RepositoryBase<Accommodation>(context), IAccommodationRepository
{
    public async Task CreateWithProcedure(AccommodationToCreateDto accommodation)
    {
        await context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Accommodation_Insert] @Name = '{accommodation.Name}'");
    }
}