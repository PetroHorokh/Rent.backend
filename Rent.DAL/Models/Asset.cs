using System;
using System.Collections.Generic;
using Rent.DAL.Models;

namespace Rent.DAL;

public partial class Asset
{
    public Guid AssetId { get; set; }

    public Guid OwnerId { get; set; }

    public Guid RoomId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Owner Owner { get; set; } = null!;

    public virtual ICollection<Rent> Rents { get; set; } = new List<Rent>();

    public virtual Room Room { get; set; } = null!;
}
