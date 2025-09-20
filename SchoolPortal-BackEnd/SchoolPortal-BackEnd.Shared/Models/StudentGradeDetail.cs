using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class StudentGradeDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid StudentGUID { get; set; }

    public Guid SessionId { get; set; }

    public Guid CategoryId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeQ1 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeQ2 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeQ3 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeFA1 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeFA2 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeFA3 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeFA4 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeSA1 { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string GradeSA2 { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

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
}
