using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class NotificationReceiverMaster
{
    public Guid NotificationReceiverId { get; set; }

    public string? NotificationReceiverName { get; set; }

    public string? NotificationReceiverDesc { get; set; }

    public int NotificationReceiverCmpId { get; set; }

    public int? NotificationReceiverSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
