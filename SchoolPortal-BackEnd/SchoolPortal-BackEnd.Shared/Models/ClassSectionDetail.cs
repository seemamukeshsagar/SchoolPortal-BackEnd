using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ClassSectionDetail")]
public partial class ClassSectionDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid ClassMasterId { get; set; }
    
    [ForeignKey("ClassMasterId")]
    public virtual ClassMaster Class { get; set; }

    public Guid SectionMasterId { get; set; }
    
    [ForeignKey("SectionMasterId")]
    public virtual SectionMaster Section { get; set; }

    public Guid LocationId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CompanyId { get; set; }
    
    [ForeignKey("CompanyId")]
    public virtual CompanyMaster Company { get; set; }

    public Guid SchoolId { get; set; }
    
    [ForeignKey("SchoolId")]
    public virtual SchoolMaster School { get; set; }

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
}
