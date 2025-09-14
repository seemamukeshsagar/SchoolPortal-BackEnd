using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class TimeTableSetupDetail
{
    public Guid Id { get; set; }

    public TimeOnly SchoolStartTime { get; set; }

    public TimeOnly SchoolEndTime { get; set; }

    public TimeOnly PeriodStartTime { get; set; }

    public int TotalPeriods { get; set; }

    public int PeriodDuration { get; set; }

    public int RecessDuration { get; set; }

    public int RecessAfterPeriod { get; set; }

    public int? FruitRecessDuration { get; set; }

    public int? FruitRecessAfterPeriod { get; set; }

    public Guid SessionId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }

    public virtual UserDetail? CreatedByNavigation { get; set; }

    public virtual UserDetail? ModifiedByNavigation { get; set; }
}
