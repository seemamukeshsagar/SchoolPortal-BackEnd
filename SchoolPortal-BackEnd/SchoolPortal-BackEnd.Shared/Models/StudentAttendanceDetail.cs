using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class StudentAttendanceDetail
{
    public Guid Id { get; set; }

    public Guid StudentGuid { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public DateTime AttemdenceDate { get; set; }

    public bool Status { get; set; }

    public int? AttendanceReasonId { get; set; }

    public string? AttendenceTime { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? StatusMessage { get; set; }
}
