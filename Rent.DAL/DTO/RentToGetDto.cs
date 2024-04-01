namespace Rent.DAL.DTO;

public class RentToGetDto
{
    public int Number { get; set; }

    public string RoomType { get; set; } = null!;

    public Guid CreatedBy { get; set; }
}