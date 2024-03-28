using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class AccommodationRepository(RentContext context) : RepositoryBase<Accommodation>(context), IAccommodationRepository
{
    public async Task CreateWithProcedure(AccommodationToCreateDto accommodation)
    {
        await Context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Accommodation_Insert] @Name = '{accommodation.Name}', @CreatedBy = '{accommodation.CreatedBy}'");
    }
}