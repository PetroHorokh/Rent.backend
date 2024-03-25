using System;
using System.Collections.Generic;

namespace Rent.DAL;

public partial class Accommodation
{
    public int AccommodationId { get; set; }

    public string Name { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual ICollection<AccommodationRoom> AccommodationRooms { get; set; } = new List<AccommodationRoom>();
}
