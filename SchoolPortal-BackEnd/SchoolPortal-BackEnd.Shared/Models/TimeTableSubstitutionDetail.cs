using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class TimeTableSubstitutionDetail
{
    public Guid Id { get; set; }

    public Guid PeriodId { get; set; }

    public Guid TeacherId { get; set; }

    public Guid TeacherNewId { get; set; }

    public Guid SubjectId { get; set; }

    public Guid ClassMasterId { get; set; }

    public Guid SectionMasterId { get; set; }

    public int DayOfWeek { get; set; }

    public DateTime SubstitutionDate { get; set; }

    public Guid SessionId { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }
}
