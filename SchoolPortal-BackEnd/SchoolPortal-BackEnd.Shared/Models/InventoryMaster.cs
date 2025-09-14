using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class InventoryMaster
{
    public Guid InvId { get; set; }

    public string? InvName { get; set; }

    public int InvItemId { get; set; }

    public int InvItemLocationId { get; set; }

    public int? InvQuantity { get; set; }

    public decimal? InvCostPerItem { get; set; }

    public bool? InvIsActive { get; set; }

    public int InvCmpId { get; set; }

    public int InvSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
