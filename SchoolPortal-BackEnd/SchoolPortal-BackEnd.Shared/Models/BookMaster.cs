using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("BookMaster")]
[Index("ISBNNumber", Name = "UQ_BookMaster_ISBNNumber", IsUnique = true)]
public partial class BookMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Title { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Description { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Image { get; set; }

    public Guid TypeId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid AuthorId { get; set; }

    public Guid PublisherId { get; set; }

    public Guid? SupplierId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Edition { get; set; }

    public int? NoOfCopies { get; set; }

    public int? StockInHand { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PublishingDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ISBNNumber { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public int? TotalPages { get; set; }

    public bool IsIssuable { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CallNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AccessionNumber { get; set; }

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
