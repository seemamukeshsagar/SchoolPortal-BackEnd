using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class ItemTypeMaster
{
    public Guid ItemTypeId { get; set; }

    public string? ItemTypeName { get; set; }

    public string? ItemTypeDescription { get; set; }

    public bool? ItemTypeIsActive { get; set; }

    public int ItemTypeCmpId { get; set; }

    public int ItemTypeSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
