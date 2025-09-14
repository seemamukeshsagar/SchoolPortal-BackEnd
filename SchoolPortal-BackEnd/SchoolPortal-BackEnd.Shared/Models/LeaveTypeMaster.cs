using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class LeaveTypeMaster
{
    public Guid Id { get; set; }

    public string? LeaveTypeCode { get; set; }

    public string? LeaveTypeDescription { get; set; }

    public string? ApplicableGender { get; set; }

    public bool IsSpecialLeave { get; set; }

    public bool IsEncashable { get; set; }

    public bool IsCarryForward { get; set; }

    public bool LeaveTypeIsActive { get; set; }

    public Guid LeaveTypeCompanyId { get; set; }

    public Guid LeaveTypeSchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }
}
