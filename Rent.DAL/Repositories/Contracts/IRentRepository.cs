using Rent.DAL.DTO;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface IRentRepository : IRepositoryBase<Models.Rent>
{
    Task CreateWithProcedure(RentToCreateDto rent);
}