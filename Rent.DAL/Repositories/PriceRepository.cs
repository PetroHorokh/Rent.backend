using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;
using System.Net;

namespace Rent.DAL.Repositories;

public class PriceRepository(RentContext context) : RepositoryBase<Price>(context), IPriceRepository
{
    public async Task CreateWithProcedure(PriceToCreateDto price)
    {
        await context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Price_Insert] @StartDate = '{price.StartDate}', @Value = {price.Value}, @EndDate = '{price.EndDate}', @RoomTypeId = '{price.RoomTypeId}'");
    }
}