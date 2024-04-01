using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface IAccommodationRoomRepository : IRepositoryBase<AccommodationRoom>
{
    Task<RepositoryResponseDto> CreateWithProcedure(AccommodationRoomToCreateDto accommodationRoom);
}