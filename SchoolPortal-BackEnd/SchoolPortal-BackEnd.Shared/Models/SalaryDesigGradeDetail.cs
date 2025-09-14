using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class SalaryDesigGradeDetail
{
    public Guid SaldgdId { get; set; }

    public int SaldgdDesgradeId { get; set; }

    public int SaldgdSalhmId { get; set; }

    public decimal? SaldgdValue { get; set; }

    public string? SaldgdSession { get; set; }

    public bool? SaldgdIsActive { get; set; }

    public int SaldgdCmpId { get; set; }

    public int SaldgdSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
