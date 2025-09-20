using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class StudentCommentDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid StudentGUID { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    public Guid SessionId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string DescriptionQtr1 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string DescriptionQtr2 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string DescriptionQtr3 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string DescriptionSA1 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string DescriptionSA2 { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string DescriptionFinal { get; set; }

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
