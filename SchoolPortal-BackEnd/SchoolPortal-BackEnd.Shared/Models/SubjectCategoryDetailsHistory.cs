using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class SubjectCategoryDetailsHistory
{
    public Guid Id { get; set; }

    public Guid SubjectCategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? ParentId { get; set; }

    public int? SubjectId { get; set; }

    public Guid SessionId { get; set; }

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
