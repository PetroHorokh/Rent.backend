using Rent.DAL.Models;

namespace Rent.BLL.Services.Contracts;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByNumberAsync(int roomNumber);
}