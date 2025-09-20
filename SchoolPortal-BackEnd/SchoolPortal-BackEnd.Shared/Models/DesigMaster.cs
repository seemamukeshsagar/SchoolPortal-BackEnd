using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("DesigMaster")]
public partial class DesigMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Code { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; }

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
    [InverseProperty("DesigMasters")]
    public virtual CompanyMaster Company { get; set; }

    [InverseProperty("Designation")]
    public virtual ICollection<DeptDesigDetail> DeptDesigDetails { get; set; } = new List<DeptDesigDetail>();

    [InverseProperty("Designation")]
    public virtual ICollection<DesigGradeDetail> DesigGradeDetails { get; set; } = new List<DesigGradeDetail>();

    [ForeignKey("SchoolId")]
    [InverseProperty("DesigMasters")]
    public virtual SchoolMaster School { get; set; }

    [InverseProperty("Designation")]
    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
