namespace Rent.DAL.DTO;

public class AccommodationRoomToGetDto
{
    public Guid AccommodationRoomId { get; set; }
    public int AccommodationId { get; set; }
    public Guid RoomId { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }

    public override string ToString() =>
        $"\nAccommodation registry id: {AccommodationRoomId}\nAccommodation {AccommodationId} has {Name} and is in quantity of {Quantity}";
}