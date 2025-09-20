using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("FeeClassDetailsHistory")]
public partial class FeeClassDetailsHistory
{
    [Key]
    public int FCDH_ID { get; set; }

    public int FCDH_CM_ID { get; set; }

    public int FCDH_FEE_CAT_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FCDH_FROM_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FCDH_TO_DATE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FCDH_AMOUNT { get; set; }

    public bool FCDH_IS_ACTIVE { get; set; }

    public int? FCDH_SCH_ID { get; set; }

    public int? FCDH_CMP_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FCDH_SESSION { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CREATED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CREATED_DATE { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string MODIFIED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime MODIFIED_DATE { get; set; }

    [ForeignKey("FCDH_FEE_CAT_ID")]
    [InverseProperty("FeeClassDetailsHistories")]
    public virtual FeesCategoryMaster FCDH_FEE_CAT { get; set; }
}
