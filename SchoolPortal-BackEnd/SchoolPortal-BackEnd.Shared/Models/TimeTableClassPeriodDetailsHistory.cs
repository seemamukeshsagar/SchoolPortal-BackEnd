using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class TimeTableClassPeriodDetailsHistory
{
    public Guid TtclpdhId { get; set; }

    public int TtclpdhCmId { get; set; }

    public int TtclpdhSecId { get; set; }

    public int? TtclpdhSubId { get; set; }

    public int TtclpdhPeriodId { get; set; }

    public int TtclpdhDayOfWeek { get; set; }

    public int TtclpdhSessionId { get; set; }

    public int TtclpdhCmpId { get; set; }

    public int TtclpdhSchId { get; set; }

    public string? TtclpdhSession { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
