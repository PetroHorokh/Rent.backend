using Rent.DAL.DTO;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface IAccommodationRepository : IRepositoryBase<Accommodation>
{
    Task CreateWithProcedure(AccommodationToCreateDto accommodation);
}