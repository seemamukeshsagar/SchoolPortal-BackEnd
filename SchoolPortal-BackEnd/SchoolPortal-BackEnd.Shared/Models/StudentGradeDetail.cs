using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class StudentGradeDetail
{
    public Guid Id { get; set; }

    public Guid StudentGuid { get; set; }

    public Guid SessionId { get; set; }

    public Guid CategoryId { get; set; }

    public string? GradeQ1 { get; set; }

    public string? GradeQ2 { get; set; }

    public string? GradeQ3 { get; set; }

    public string? GradeFa1 { get; set; }

    public string? GradeFa2 { get; set; }

    public string? GradeFa3 { get; set; }

    public string? GradeFa4 { get; set; }

    public string? GradeSa1 { get; set; }

    public string? GradeSa2 { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

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
}
