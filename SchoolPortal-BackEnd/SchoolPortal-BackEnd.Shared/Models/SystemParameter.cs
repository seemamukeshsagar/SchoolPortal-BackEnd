using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class SystemParameter
{
    public Guid SysParameterId { get; set; }

    public string SyspParameterName { get; set; } = null!;

    public string? SyspParameterValue { get; set; }

    public string? SyspParameterDescription { get; set; }

    public int SyspCmpId { get; set; }

    public int SyspSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
