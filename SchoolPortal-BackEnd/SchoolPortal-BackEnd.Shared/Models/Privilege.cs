using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class Privilege
{
    public Guid Id { get; set; }

    public string? PrivilegeName { get; set; }

    public int? PrivilegeParentId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }

    public virtual ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();
}
