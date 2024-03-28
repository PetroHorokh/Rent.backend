using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface IAssetRepository : IRepositoryBase<Asset>
{
    Task CreateWithProcedure(AssetToCreateDto asset);
}