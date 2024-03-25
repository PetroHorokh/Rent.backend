using System;
using System.Collections.Generic;

namespace Rent.DAL;

public partial class Owner
{
    public Guid OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public Guid AddressId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
