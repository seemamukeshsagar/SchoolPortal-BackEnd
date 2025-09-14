using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class SmtpDetail
{
    public Guid SmtpDetailId { get; set; }

    public string? SmtpDetailFromAddress { get; set; }

    public string? SmtpDetailGateway { get; set; }

    public string? SmtpDetailUsername { get; set; }

    public string? SmtpDetailPassword { get; set; }

    public string? SmtpDetailSubject { get; set; }

    public string? SmtpDetailBodyText { get; set; }

    public string? SmtpDetailEmailType { get; set; }

    public bool? SmtpDetailIsActive { get; set; }

    public int SmtpDetailCmpId { get; set; }

    public int SmtpDetailSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
