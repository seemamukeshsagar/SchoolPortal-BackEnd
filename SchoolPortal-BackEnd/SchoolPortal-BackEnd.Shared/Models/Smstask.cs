using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class Smstask
{
    public Guid StsId { get; set; }

    public string? StsName { get; set; }

    public string? StsDescription { get; set; }

    public string StsNotificationSendEmail { get; set; } = null!;

    public string StsNotificationSendSms { get; set; } = null!;

    public int StsNotificationReceieverId { get; set; }

    public string StsRepeatSchedule { get; set; } = null!;

    public int StsStatusId { get; set; }

    public bool StsIsActive { get; set; }

    public int StsCmpId { get; set; }

    public int StsSchId { get; set; }

    public bool? StsIsReadonly { get; set; }

    public int? StsLastRunStatusId { get; set; }

    public DateTime? StsLastRunDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
