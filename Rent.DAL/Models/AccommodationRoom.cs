using System;
using System.Collections.Generic;
using Rent.DAL.Models;

namespace Rent.DAL;

public partial class AccommodationRoom
{
    public Guid AccommodationRoomId { get; set; }

    public int AccommodationId { get; set; }

    public Guid RoomId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual Accommodation Accommodation { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
