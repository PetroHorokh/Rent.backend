using System;
using System.Collections.Generic;
using Rent.DAL.Models;

namespace Rent.DAL;

public partial class Price
{
    public Guid PriceId { get; set; }

    public DateTime StartDate { get; set; }

    public decimal Value { get; set; }

    public DateTime? EndDate { get; set; }

    public int RoomTypeId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDateTime { get; set; }

    public virtual RoomType RoomType { get; set; } = null!;
}
