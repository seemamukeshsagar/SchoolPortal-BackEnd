using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class FeesDiscountCategoryMaster
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid FeeCategoryId { get; set; }

    public bool? IsPercentAge { get; set; }

    public decimal? Amount { get; set; }

    public Guid CompanyMasterId { get; set; }

    public Guid SchoolMasterId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }
}
