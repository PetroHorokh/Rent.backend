using System;
using System.Collections.Generic;

namespace Rent.DAL;

public partial class Impost
{
    public Guid ImpostId { get; set; }

    public decimal Tax { get; set; }

    public decimal Fine { get; set; }

    public int PaymentDay { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }
}
