using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("AssesmentMaster")]
public partial class AssesmentMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PercentageWeightage { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FromPeriod { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ToPeriod { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }
}
