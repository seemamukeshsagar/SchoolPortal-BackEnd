using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class DesigMaster
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

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

    public virtual CompanyMaster Company { get; set; } = null!;

    public virtual ICollection<DeptDesigDetail> DeptDesigDetails { get; set; } = new List<DeptDesigDetail>();

    public virtual ICollection<DesigGradeDetail> DesigGradeDetails { get; set; } = new List<DesigGradeDetail>();

    public virtual SchoolMaster School { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
