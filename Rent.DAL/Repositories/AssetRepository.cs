using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class AssetRepository(RentContext context) : RepositoryBase<Asset>(context), IAssetRepository
{
    public async Task CreateWithProcedure(AssetToCreateDto asset)
    {
        await Context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Asset_Insert] @OwnerId = '{asset.OwnerId}', @RoomId = '{asset.RoomId}', @CreatedBy = '{asset.CreatedBy}'");
    }
}