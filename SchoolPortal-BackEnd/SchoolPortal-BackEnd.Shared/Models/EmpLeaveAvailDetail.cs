using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class EmpLeaveAvailDetail
{
    public Guid Id { get; set; }

    public int EmployeeId { get; set; }

    public int LeaveTypeId { get; set; }

    public DateTime ApplyDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal? TotalDays { get; set; }

    public bool IsHalfDay { get; set; }

    public string? LeaveReason { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    public string? ZipCode { get; set; }

    public string? ContactNumber { get; set; }

    public int StatusId { get; set; }

    public string? Session { get; set; }

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
