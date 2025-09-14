using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class ItemMaster
{
    public Guid ItemId { get; set; }

    public string? ItemName { get; set; }

    public string? ItemDescription { get; set; }

    public int? ItemTypeMasterId { get; set; }

    public bool? ItemIsActive { get; set; }

    public int ItemCmpId { get; set; }

    public int ItemSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
