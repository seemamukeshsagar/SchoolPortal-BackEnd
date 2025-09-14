using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class TimeTableDetailsHistory
{
    public Guid TtdetailhId { get; set; }

    public int TtdetailhPeriodId { get; set; }

    public int TtdetailhTeacherId { get; set; }

    public int TtdetailhCmId { get; set; }

    public int TtdetailhSecId { get; set; }

    public int TtdetailhSubId { get; set; }

    public int TtdetailhDayOfWeek { get; set; }

    public int TtdetailhCmpId { get; set; }

    public int TtdetailhSchId { get; set; }

    public int TtdetailhSessionId { get; set; }

    public string? TtdetailhSession { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
