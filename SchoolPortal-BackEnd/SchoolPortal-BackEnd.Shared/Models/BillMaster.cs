﻿using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class BillMaster
{
    public Guid Id { get; set; }

    public string BillNumber { get; set; } = null!;

    public DateTime BillDate { get; set; }

    public DateTime? BillDueDate { get; set; }

    public string? Description { get; set; }

    public Guid VendorId { get; set; }

    public decimal? BillAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? TotalBillAmount { get; set; }

    public Guid VehicleId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }
}
