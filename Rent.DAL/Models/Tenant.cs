using System;
using System.Collections.Generic;

namespace Rent.DAL.Models;

public partial class Tenant
{
    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public Guid AddressId { get; set; }

    public string Director { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Rent> Rents { get; set; } = new List<Rent>();
}
