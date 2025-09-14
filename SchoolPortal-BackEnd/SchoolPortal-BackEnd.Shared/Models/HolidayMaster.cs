using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class HolidayMaster
{
    public Guid Id { get; set; }

    public string? HolidayName { get; set; }

    public string? HolidayDescription { get; set; }

    public Guid HolidayTypeId { get; set; }

    public DateTime HolidayFromDate { get; set; }

    public DateTime HolidayToDate { get; set; }

    public Guid HolidayYear { get; set; }

    public bool? HolidayIsActive { get; set; }

    public Guid HolidayCompanyId { get; set; }

    public Guid HolidaySchoolId { get; set; }

    public bool? HolidayIsStaffApplicable { get; set; }

    public Guid SessionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }
}
