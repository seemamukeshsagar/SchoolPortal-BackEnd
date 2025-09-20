using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class StudentFeeDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid StudentGUID { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaidDate { get; set; }

    public bool IsPaid { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LateFeeAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FeeReceiptNumber { get; set; }

    public Guid Month { get; set; }

    public Guid Year { get; set; }

    public Guid SchoolId { get; set; }

    public Guid CompanyId { get; set; }

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
