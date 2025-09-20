using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("FeesDiscountCategoryMaster")]
public partial class FeesDiscountCategoryMaster
{
    [Key]
    public Guid FeesDiscountCategoryMasterId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string FeesDiscountCategoryMasterName { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string FeesDiscountCategoryMasterDescription { get; set; }

    public Guid FeesDiscountCategoryMasterFeeCategoryId { get; set; }

    public bool? FeesDiscountCategoryMasterIsPercentAge { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FeesDiscountCategoryMasterAmount { get; set; }

    public Guid FeesDiscountCategoryMasterCompanyMasterId { get; set; }

    public Guid FeesDiscountCategoryMasterSchoolMasterId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string StatusMessage { get; set; }
}
