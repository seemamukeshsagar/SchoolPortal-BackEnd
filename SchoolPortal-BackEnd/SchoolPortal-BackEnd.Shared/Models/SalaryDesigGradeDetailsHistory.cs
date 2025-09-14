using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class SalaryDesigGradeDetailsHistory
{
    public Guid SaldgdhId { get; set; }

    public int SaldgdhDesgradeId { get; set; }

    public int SaldgdhSalhmId { get; set; }

    public decimal? SaldgdhValue { get; set; }

    public string? SaldgdhSession { get; set; }

    public bool? SaldgdhIsActive { get; set; }

    public int SaldgdhCmpId { get; set; }

    public int SaldgdhSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
