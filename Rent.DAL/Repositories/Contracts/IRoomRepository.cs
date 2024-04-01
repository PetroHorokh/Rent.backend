using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface IRoomRepository : IRepositoryBase<Room>
{
    Task<RepositoryResponseDto> CreateWithProcedure(RoomToCreateDto room);
}