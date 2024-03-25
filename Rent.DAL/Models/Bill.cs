using System;
using System.Collections.Generic;
using Rent.DAL.Models;

namespace Rent.DAL;

public partial class Bill
{
    public Guid BillId { get; set; }

    public Guid TenantId { get; set; }

    public Guid AssetId { get; set; }

    public decimal BillAmount { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Tenant Tenant { get; set; } = null!;
}
