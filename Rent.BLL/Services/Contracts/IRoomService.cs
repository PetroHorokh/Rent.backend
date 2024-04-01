using Rent.DAL.DTO;
using Rent.DAL.Models;

namespace Rent.BLL.Services.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomToGetDto>> GetAllRoomsAsync();
    Task<IEnumerable<RoomTypeToGetDto>> GetAllRoomTypesAsync();
    Task<RoomToGetDto?> GetRoomByRoomIdAsync(Guid roomId);
    Task<RoomToGetDto?> GetRoomByNumberAsync(int roomNumber);
    Task<IEnumerable<AccommodationRoomToGetDto>> GetAccommodationsOfRoomAsync(Guid roomId);
    Task<AccommodationRoomToGetDto?> GetAccommodationRoomByIdAsync(Guid accommodationRoomId);
    Task<RepositoryResponseDto> CreateRoomAsync(RoomToCreateDto room);
    Task<RepositoryResponseDto> DeleteRoom(Guid roomId);
    Task<IEnumerable<AccommodationToGetDto>> GetAllAccommodationsAsync();
    Task<RepositoryResponseDto> CreateAccommodationRoomAsync(AccommodationRoomToCreateDto accommodationRoom);
    
    Task<RepositoryResponseDto> ChangeQuantityOfAccommodationAsync(AccommodationRoomToUpdateDto accommodationRoom);
}