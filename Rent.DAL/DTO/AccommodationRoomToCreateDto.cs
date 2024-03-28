namespace Rent.DAL.DTO;

public class AccommodationRoomToCreateDto
{
    public int AccommodationId { get; set; }

    public Guid RoomId { get; set; }

    public Guid CreatedBy { get; set; }
}