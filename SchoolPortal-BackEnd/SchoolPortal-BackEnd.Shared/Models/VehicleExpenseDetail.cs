using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class VehicleExpenseDetail
{
    public Guid Id { get; set; }

    public int VehicleId { get; set; }

    public Guid VehicleTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? ExpenseDate { get; set; }

    public decimal? ExpenseAmount { get; set; }

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
