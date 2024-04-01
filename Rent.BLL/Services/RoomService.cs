using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rent.BLL.Services.Contracts;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.UnitOfWork;

namespace Rent.BLL.Services;

public class RoomService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ViewService> logger) : IRoomService
{
    public async Task<IEnumerable<RoomToGetDto>> GetAllRoomsAsync()
    {
        logger.LogInformation("Entering RoomService, GetAllRoomsAsync");

        logger.LogInformation("Calling RoomRepository, method GetAllAsync");
        var rooms = await unitOfWork.Rooms.GetAllAsync();
        logger.LogInformation("Finished calling RoomRepository, method GetAllAsync");

        logger.LogInformation($"Mapping rooms to RoomToGetDto");
        var result = rooms.Select(mapper.Map<RoomToGetDto>);

        logger.LogInformation("Exiting RoomService, GetAllRoomsAsync");
        return result;
    }

    public async Task<IEnumerable<RoomTypeToGetDto>> GetAllRoomTypesAsync()
    {
        logger.LogInformation("Entering RoomService, GetAllRoomTypesAsync");

        logger.LogInformation("Calling RoomTypeRepository, method GetAllAsync");
        var roomTypes = await unitOfWork.RoomTypes.GetAllAsync();
        logger.LogInformation("Finished calling RoomTypeRepository, method GetAllAsync");

        logger.LogInformation($"Mapping room types to RoomToGetDto");
        var result = roomTypes.Select(mapper.Map<RoomTypeToGetDto>);

        logger.LogInformation("Exiting RoomService, GetAllRoomsAsync");
        return result;
    }

    public async Task<RoomToGetDto?> GetRoomByRoomIdAsync(Guid roomId)
    {
        logger.LogInformation("Entering RoomService, GetRoomByRoomIdAsync");

        logger.LogInformation("Calling RoomRepository, method GetSingleByConditionAsync");
        var room = await unitOfWork.Rooms.GetSingleByConditionAsync(room => room.RoomId == roomId);
        logger.LogInformation("Finished calling RoomRepository, method GetSingleByConditionAsync");

        logger.LogInformation($"Mapping room to RoomToGetDto");
        var result = mapper.Map<RoomToGetDto>(room);

        logger.LogInformation("Exiting RoomService, GetRoomByRoomIdAsync");
        return result;
    }

    public async Task<RoomToGetDto?> GetRoomByNumberAsync(int roomNumber)
    {
        logger.LogInformation("Entering RoomService, GetRoomByNumberAsync");

        logger.LogInformation("Calling RoomRepository, method GetSingleByConditionAsync");
        var room = await unitOfWork.Rooms.GetSingleByConditionAsync(room => room.Number == roomNumber);
        logger.LogInformation("Finished calling RoomRepository, method GetSingleByConditionAsync");

        logger.LogInformation($"Mapping room to RoomToGetDto");
        var result = mapper.Map<RoomToGetDto>(room);

        logger.LogInformation("Exiting RoomService, GetRoomByNumberAsync");
        return result;
    }

    public async Task<AccommodationRoomToGetDto?> GetAccommodationRoomByIdAsync(Guid accommodationRoomId)
    {
        logger.LogInformation("Entering RoomService, GetAccommodationRoomByIdAsync");

        logger.LogInformation("Calling AccommodationRoomRepository, method GetSingleByConditionAsync");
        var room = await unitOfWork.AccommodationRooms.GetSingleByConditionAsync(accommodationRoom => accommodationRoom.AccommodationRoomId == accommodationRoomId);
        logger.LogInformation("Finished calling RoomRepository, method GetSingleByConditionAsync");

        logger.LogInformation($"Mapping room to AccommodationRoomToGetDto");
        var result = mapper.Map<AccommodationRoomToGetDto>(room);

        logger.LogInformation("Exiting RoomService, GetAccommodationRoomByIdAsync");
        return result;
    }

    public async Task<IEnumerable<AccommodationRoomToGetDto>> GetAccommodationsOfRoomAsync(Guid roomId)
    {
        logger.LogInformation("Entering RoomService, GetAccommodationsOfRoomAsync");

        logger.LogInformation("Calling AccommodationRoomRepository, method GetByConditionAsync");
        var accommodations = await unitOfWork.AccommodationRooms.GetByConditionAsync(accommodation => accommodation.RoomId == roomId, accommodation => accommodation.Accommodation);
        logger.LogInformation("Finished calling AccommodationRoomRepository, method GetByConditionAsync");

        logger.LogInformation($"Mapping accommodations to AccommodationRoomToGetDto");
        var result = accommodations.Select(mapper.Map<AccommodationRoomToGetDto>);

        logger.LogInformation("Exiting RoomService, GetAccommodationsOfRoomAsync");
        return result;
    }

    public async Task<RepositoryResponseDto> CreateRoomAsync(RoomToCreateDto room)
    {
        logger.LogInformation("Entering RoomService, CreateRoom");
        
        logger.LogInformation("Calling RoomRepository, method CreateWithProcedure");
        logger.LogInformation($"Parameters: @Number = {room.Number}, @Area = {room.Area}, @RoomTypeId = {room.RoomTypeId}");
        var result = await unitOfWork.Rooms.CreateWithProcedure(room);
        logger.LogInformation("Finished calling RoomRepository, method CreateWithProcedure");
        
        logger.LogInformation("Exiting RoomService, CreateRoom");
        return result;
    }

    public async Task<RepositoryResponseDto> DeleteRoom(Guid roomId)
    {
        logger.LogInformation("Entering RoomService, DeleteRoom");

        DbUpdateException? error = null;

        logger.LogInformation("Calling RoomRepository, method GetSingleByConditionAsync");
        logger.LogInformation($"Parameter: RoomId = {roomId}");
        var room = await unitOfWork.Rooms.GetSingleByConditionAsync(room => room.RoomId == roomId);
        logger.LogInformation("Finished calling RoomRepository, method GetSingleByConditionAsync");
        try
        {
            if (room != null)
            {
                logger.LogInformation("Calling RoomRepository, method Delete");
                unitOfWork.Rooms.Delete(room);
                logger.LogInformation("Finished calling RoomRepository, method Delete");

                await unitOfWork.SaveAsync();
            }
        }
        catch (DbUpdateException ex)
        {
            logger.LogInformation($"An error occured while deleting Room entity: {ex.InnerException}");
            error = ex;
        }

        logger.LogInformation("Exiting RoomService, DeleteRoom");
        return new RepositoryResponseDto(){ DateTime = DateTime.Now, Error = error};
    }

    public async Task<IEnumerable<AccommodationToGetDto>> GetAllAccommodationsAsync()
    {
        logger.LogInformation("Entering RoomService, GetAllAccommodationsAsync");

        logger.LogInformation("Calling AccommodationRepository, method GetAllAsync");
        var accommodations = await unitOfWork.Accommodations.GetAllAsync();
        logger.LogInformation("Finished calling AccommodationRepository, method GetAllAsync");

        logger.LogInformation($"Mapping accommodations to AccommodationToGetDto");
        var result = accommodations.Select(mapper.Map<AccommodationToGetDto>);

        logger.LogInformation("Exiting RoomService, GetAllAccommodationsAsync");
        return result;
    }

    public async Task<RepositoryResponseDto> CreateAccommodationRoomAsync(AccommodationRoomToCreateDto accommodationRoom)
    {
        logger.LogInformation("Entering RoomService, CreateAccommodationRoom");

        logger.LogInformation("Calling AccommodationRoomRepository, method CreateWithProcedure");
        logger.LogInformation(
            $"Parameters: @AccommodationId = {accommodationRoom.AccommodationId}, @RoomId = {accommodationRoom.RoomId}, @Quantity = {accommodationRoom.Quantity}");
        var result = await unitOfWork.AccommodationRooms.CreateWithProcedure(accommodationRoom);
        logger.LogInformation("Finished calling RoomRepository, method CreateWithProcedure");

        logger.LogInformation("Exiting RoomService, CreateRoom");
        return result;
    }

    public async Task<RepositoryResponseDto> ChangeQuantityOfAccommodationAsync(AccommodationRoomToUpdateDto accommodationRoom)
    {
        logger.LogInformation("Entering RoomService, ChangeQuantityOfAccommodationAsync");

        DbUpdateException? error = null;

        logger.LogInformation("Calling AccommodationRoomRepository, method GetSingleByConditionAsync");
        logger.LogInformation($"Parameters: Quantity = {accommodationRoom.Quantity}");
        var accommodation =
            await unitOfWork.AccommodationRooms.GetSingleByConditionAsync(accommodation => accommodation.AccommodationRoomId == accommodationRoom.AccommodationRoomId);
        logger.LogInformation("Finished calling AccommodationRoomRepository, method GetSingleByConditionAsync");

        try
        {
            if (accommodation != null)
            {
                accommodation.Quantity = accommodationRoom.Quantity;

                logger.LogInformation("Calling AccommodationRoomRepository, method Update");
                unitOfWork.AccommodationRooms.Update(accommodation);
                logger.LogInformation("Finished calling AccommodationRoomRepository, method Update");

                await unitOfWork.SaveAsync();

                logger.LogInformation("Entering RoomService, ChangeQuantityOfAccommodationAsync");
            }
        }
        catch (DbUpdateException ex)
        {
            logger.LogInformation($"An error occured while updating AccommodationRoom entity: {ex.InnerException}");
            error = ex;
        }

        logger.LogInformation("Exiting RoomService, ChangeQuantityOfAccommodationAsync");
        return new RepositoryResponseDto() { DateTime = DateTime.Now, Error = error };
    }
}