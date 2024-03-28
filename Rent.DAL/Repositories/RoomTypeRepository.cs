using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;
using System.Net;

namespace Rent.DAL.Repositories;

public class RoomTypeRepository(RentContext context) : RepositoryBase<RoomType>(context), IRoomTypeRepository
{
    public async Task CreateWithProcedure(RoomTypeToCreateDto roomType)
    {
        await context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_RoomType_Insert] @Name = '{roomType.Name}', @CreatedBy = '{roomType.CreatedBy}'");
    }
}