using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class EmpSalaryStructureDetailsHistory
{
    public Guid Id { get; set; }

    public Guid EmployeeId { get; set; }

    public Guid DesignationGradeId { get; set; }

    public string? Session { get; set; }

    public decimal? Value { get; set; }

    public string SalaryType { get; set; } = null!;

    public bool IsDeductance { get; set; }

    public string? SalaryCode { get; set; }

    public string? Description { get; set; }

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
