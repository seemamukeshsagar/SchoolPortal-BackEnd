using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class DesigGradeDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid DesignationId { get; set; }

    public Guid GradeId { get; set; }

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
    [InverseProperty("DesigGradeDetails")]
    public virtual CompanyMaster Company { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("DesigGradeDetailCreatedByNavigations")]
    public virtual UserDetail CreatedByNavigation { get; set; }

    [ForeignKey("DesignationId")]
    [InverseProperty("DesigGradeDetails")]
    public virtual DesigMaster Designation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("DesigGradeDetailModifiedByNavigations")]
    public virtual UserDetail ModifiedByNavigation { get; set; }

    [ForeignKey("SchoolId")]
    [InverseProperty("DesigGradeDetails")]
    public virtual SchoolMaster School { get; set; }
}
