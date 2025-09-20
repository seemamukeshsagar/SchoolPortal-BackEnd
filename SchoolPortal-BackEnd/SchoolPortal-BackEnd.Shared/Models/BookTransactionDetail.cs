using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class BookTransactionDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime IssueDate { get; set; }

    public int IssueDays { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReturnDueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActualReturnDate { get; set; }

    public int? LateDays { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FinePerDay { get; set; }

    public bool? IsFineApplicable { get; set; }

    public bool? IsFinePaid { get; set; }

    public Guid? BookTransactionTypeId { get; set; }

    public Guid? ClassMasterId { get; set; }

    public Guid? SectionMasterId { get; set; }

    public Guid? StudentGuid { get; set; }

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
}
