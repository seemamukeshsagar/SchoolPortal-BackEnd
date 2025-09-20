using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class TeacherQualificationDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid TeacherId { get; set; }
    
    [ForeignKey("TeacherId")]
    public virtual TeacherMaster Teacher { get; set; }

    public Guid QualifcationId { get; set; }
    
    [ForeignKey("QualifcationId")]
    public virtual QualificationMaster Qualification { get; set; }

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
}
