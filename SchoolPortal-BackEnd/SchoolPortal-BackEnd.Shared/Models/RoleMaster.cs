using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("RoleMaster")]
public partial class RoleMaster
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string RoleMasterName { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string RoleMasterDescription { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

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
    [InverseProperty("RoleMasters")]
    public virtual CompanyMaster Company { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();

    [ForeignKey("SchoolId")]
    [InverseProperty("RoleMasters")]
    public virtual SchoolMaster School { get; set; }

    [InverseProperty("UserRole")]
    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
