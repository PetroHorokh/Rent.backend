namespace Rent.DAL.DTO;

public class AccommodationToCreateDto
{
    public string Name { get; set; } = null!;

    public Guid CreatedBy { get; set; }
}