using Rent.DAL.DTO;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface IOwnerRepository : IRepositoryBase<Owner>
{
    Task CreateWithProcedure(OwnerToCreateDto owner);
}