using System;
using System.Collections.Generic;
using Rent.DAL.Models;

namespace Rent.DAL;

public partial class Address
{
    public Guid AddressId { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Building { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual ICollection<Owner> Owners { get; set; } = new List<Owner>();

    public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
}
