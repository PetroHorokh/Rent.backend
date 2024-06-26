﻿namespace Rent.DAL.DTO;

public class AddressToGetDto
{
    public Guid AddressId { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Building { get; set; } = null!;

    public override string ToString() =>
        $"City: {City}\nStreet: {Street}\nBuilding: {Building}\n";
}