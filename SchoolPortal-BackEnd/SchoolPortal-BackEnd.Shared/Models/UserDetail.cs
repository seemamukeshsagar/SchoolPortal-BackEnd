using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class UserDetail
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public Guid DesignationId { get; set; }

    public Guid? UserRoleId { get; set; }

    public bool? IsSuperUser { get; set; }

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

    public virtual CompanyMaster? Company { get; set; }

    public virtual ICollection<DeptDesigDetail> DeptDesigDetailCreatedByNavigations { get; set; } = new List<DeptDesigDetail>();

    public virtual ICollection<DeptDesigDetail> DeptDesigDetailModifiedByNavigations { get; set; } = new List<DeptDesigDetail>();

    public virtual ICollection<DeptMaster> DeptMasterCreatedByNavigations { get; set; } = new List<DeptMaster>();

    public virtual ICollection<DeptMaster> DeptMasterModifiedByNavigations { get; set; } = new List<DeptMaster>();

    public virtual ICollection<DesigGradeDetail> DesigGradeDetailCreatedByNavigations { get; set; } = new List<DesigGradeDetail>();

    public virtual ICollection<DesigGradeDetail> DesigGradeDetailModifiedByNavigations { get; set; } = new List<DesigGradeDetail>();

    public virtual DesigMaster Designation { get; set; } = null!;

    public virtual ICollection<GenderMaster> GenderMasterCreatedByNavigations { get; set; } = new List<GenderMaster>();

    public virtual ICollection<GenderMaster> GenderMasterModifiedByNavigations { get; set; } = new List<GenderMaster>();

    public virtual ICollection<UserDetail> InverseModifiedByNavigation { get; set; } = new List<UserDetail>();

    public virtual UserDetail? ModifiedByNavigation { get; set; }

    public virtual SchoolMaster? School { get; set; }

    public virtual ICollection<TimeTableSession> TimeTableSessionCreatedByNavigations { get; set; } = new List<TimeTableSession>();

    public virtual ICollection<TimeTableSession> TimeTableSessionModifiedByNavigations { get; set; } = new List<TimeTableSession>();

    public virtual ICollection<TimeTableSetupDetail> TimeTableSetupDetailCreatedByNavigations { get; set; } = new List<TimeTableSetupDetail>();

    public virtual ICollection<TimeTableSetupDetail> TimeTableSetupDetailModifiedByNavigations { get; set; } = new List<TimeTableSetupDetail>();

    public virtual RoleMaster? UserRole { get; set; }
}
