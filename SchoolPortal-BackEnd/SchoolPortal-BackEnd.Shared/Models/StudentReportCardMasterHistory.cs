﻿using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class StudentReportCardMasterHistory
{
    public Guid RepcardhId { get; set; }

    public int RepcardhRepcardId { get; set; }

    public Guid RepcardhStuGuid { get; set; }

    public int RepcardhCmId { get; set; }

    public int RepcardhSecId { get; set; }

    public string? RepcardhSession { get; set; }

    public string? RepcardhType { get; set; }

    public string? RepcardhTypeValue { get; set; }

    public DateTime? RepcardhPeriod { get; set; }

    public int? RepcardhCmpId { get; set; }

    public int? RepcardhSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
