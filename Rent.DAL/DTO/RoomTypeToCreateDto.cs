namespace Rent.DAL.DTO;

public class RoomTypeToCreateDto
{
    public string Name { get; set; } = null!;

    public Guid CreatedBy { get; set; }
}