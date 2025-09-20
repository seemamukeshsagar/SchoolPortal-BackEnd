using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Index("EmailAddress", Name = "UQ_UserDetails_EmailAddress", IsUnique = true)]
[Index("UserName", Name = "UQ_UserDetails_UserName", IsUnique = true)]
public partial class UserDetail
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(250)]
    [Unicode(false)]
    public string UserName { get; set; }

    [Required]
    [StringLength(256)]
    public string UserPassword { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Required]
    [StringLength(250)]
    [Unicode(false)]
    public string EmailAddress { get; set; }

    public Guid DesignationId { get; set; }

    public Guid? UserRoleId { get; set; }

    public bool? IsSuperUser { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("UserDetails")]
    public virtual CompanyMaster Company { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<DeptDesigDetail> DeptDesigDetailCreatedByNavigations { get; set; } = new List<DeptDesigDetail>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<DeptDesigDetail> DeptDesigDetailModifiedByNavigations { get; set; } = new List<DeptDesigDetail>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<DeptMaster> DeptMasterCreatedByNavigations { get; set; } = new List<DeptMaster>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<DeptMaster> DeptMasterModifiedByNavigations { get; set; } = new List<DeptMaster>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<DesigGradeDetail> DesigGradeDetailCreatedByNavigations { get; set; } = new List<DesigGradeDetail>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<DesigGradeDetail> DesigGradeDetailModifiedByNavigations { get; set; } = new List<DesigGradeDetail>();

    [ForeignKey("DesignationId")]
    [InverseProperty("UserDetails")]
    public virtual DesigMaster Designation { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<GenderMaster> GenderMasterCreatedByNavigations { get; set; } = new List<GenderMaster>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<GenderMaster> GenderMasterModifiedByNavigations { get; set; } = new List<GenderMaster>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<UserDetail> InverseModifiedByNavigation { get; set; } = new List<UserDetail>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("InverseModifiedByNavigation")]
    public virtual UserDetail ModifiedByNavigation { get; set; }

    [ForeignKey("SchoolId")]
    [InverseProperty("UserDetails")]
    public virtual SchoolMaster School { get; set; }

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TimeTableSession> TimeTableSessionCreatedByNavigations { get; set; } = new List<TimeTableSession>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<TimeTableSession> TimeTableSessionModifiedByNavigations { get; set; } = new List<TimeTableSession>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TimeTableSetupDetail> TimeTableSetupDetailCreatedByNavigations { get; set; } = new List<TimeTableSetupDetail>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<TimeTableSetupDetail> TimeTableSetupDetailModifiedByNavigations { get; set; } = new List<TimeTableSetupDetail>();

    [ForeignKey("UserRoleId")]
    [InverseProperty("UserDetails")]
    public virtual RoleMaster UserRole { get; set; }
}
