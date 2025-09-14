﻿using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class FeeClassDetail
{
    public Guid Id { get; set; }

    public int ClassMasterId { get; set; }

    public int FeesCategoryId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public decimal? Amount { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }
}
