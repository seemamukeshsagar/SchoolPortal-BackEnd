﻿using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class StudentCommentDetail
{
    public Guid Id { get; set; }

    public Guid StudentGuid { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    public Guid SessionId { get; set; }

    public string? DescriptionQtr1 { get; set; }

    public string? DescriptionQtr2 { get; set; }

    public string? DescriptionQtr3 { get; set; }

    public string? DescriptionSa1 { get; set; }

    public string? DescriptionSa2 { get; set; }

    public string? DescriptionFinal { get; set; }

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
