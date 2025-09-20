using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("DeptMaster")]
public partial class DeptMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string DeptCode { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string DeptName { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

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
    [InverseProperty("DeptMasters")]
    public virtual CompanyMaster Company { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("DeptMasterCreatedByNavigations")]
    public virtual UserDetail CreatedByNavigation { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<DeptDesigDetail> DeptDesigDetails { get; set; } = new List<DeptDesigDetail>();

    [ForeignKey("ModifiedBy")]
    [InverseProperty("DeptMasterModifiedByNavigations")]
    public virtual UserDetail ModifiedByNavigation { get; set; }

    [ForeignKey("SchoolId")]
    [InverseProperty("DeptMasters")]
    public virtual SchoolMaster School { get; set; }
}
