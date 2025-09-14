using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class ProfessionMaster
{
    public Guid ProfId { get; set; }

    public string? ProfName { get; set; }

    public bool? ProfIsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
