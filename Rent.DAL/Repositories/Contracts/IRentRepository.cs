using Rent.DAL.DTO;

namespace Rent.DAL.Repositories.Contracts;

public interface IRentRepository
{
    Task CreateWithProcedure(RentToCreateDto rent);
}