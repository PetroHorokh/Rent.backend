using Rent.BLL.Services.Contracts;
using Rent.DAL.Models;
using Rent.DAL.UnitOfWork;

namespace Rent.BLL.Services;

public class RoomService(IUnitOfWork unitOfWork) : IRoomService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Room>> GetAllRoomsAsync() => await _unitOfWork.Rooms.GetAllAsync();

    public async Task<Room?> GetRoomByNumberAsync(int roomNumber) =>
        await _unitOfWork.Rooms.GetSingleByConditionAsync(room => room.Number == roomNumber);
}