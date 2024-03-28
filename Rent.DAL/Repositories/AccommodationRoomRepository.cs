using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class AccommodationRoomRepository(RentContext context) : RepositoryBase<AccommodationRoom>(context), IAccommodationRoomRepository
{
    public async Task CreateWithProcedure(AccommodationRoomToCreateDto accommodationRoom)
    {
        await Context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Accommodation_Insert] @AccommodationId = '{accommodationRoom.AccommodationId}', @RoomId = '{accommodationRoom.RoomId}', @CreatedBy = '{accommodationRoom.CreatedBy}'");
    }
}