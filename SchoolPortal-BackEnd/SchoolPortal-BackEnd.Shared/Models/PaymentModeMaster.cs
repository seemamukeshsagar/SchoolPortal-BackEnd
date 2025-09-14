using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class PaymentModeMaster
{
    public Guid PaymentModeId { get; set; }

    public string? PaymentModeName { get; set; }

    public string? PaymentModeDesc { get; set; }

    public int PaymentModeCmpId { get; set; }

    public int PaymentModeSchId { get; set; }

    public bool PaymentModeIsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
