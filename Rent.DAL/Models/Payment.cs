using System;
using System.Collections.Generic;
using Rent.DAL.Models;

namespace Rent.DAL;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid TenantId { get; set; }

    public Guid BillId { get; set; }

    public DateTime PaymentDay { get; set; }

    public decimal Amount { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
