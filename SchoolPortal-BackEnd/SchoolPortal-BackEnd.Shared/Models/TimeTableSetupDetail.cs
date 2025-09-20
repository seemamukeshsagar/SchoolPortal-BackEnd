using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class TimeTableSetupDetail
{
    [Key]
    public Guid Id { get; set; }

    [Precision(0)]
    public TimeOnly SchoolStartTime { get; set; }

    [Precision(0)]
    public TimeOnly SchoolEndTime { get; set; }

    [Precision(0)]
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

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("TimeTableSetupDetailCreatedByNavigations")]
    public virtual UserDetail CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("TimeTableSetupDetailModifiedByNavigations")]
    public virtual UserDetail ModifiedByNavigation { get; set; }
}
