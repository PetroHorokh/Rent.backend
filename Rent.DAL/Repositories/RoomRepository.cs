using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;
using System.Net;

namespace Rent.DAL.Repositories;

public class RoomRepository(RentContext context) : RepositoryBase<Room>(context), IRoomRepository
{
    public async Task CreateWithProcedure(RoomToCreateDto room)
    {
        await context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Room_Insert] @Number = {room.Number}, @Area = {room.Area}, @RoomTypeId = '{room.RoomTypeId}', @CreatedBy = '{room.CreatedBy}'");
    }
}