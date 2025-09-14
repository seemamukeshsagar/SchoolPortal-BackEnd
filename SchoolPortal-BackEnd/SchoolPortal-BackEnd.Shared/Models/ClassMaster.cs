﻿using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class ClassMaster
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? ExamAssessment { get; set; }

    public bool? IsGradePointApplicable { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? OrderBy { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }
}
