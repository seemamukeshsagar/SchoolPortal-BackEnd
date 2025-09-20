using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class FeeClassDetail
{
    [Key]
    public int FCD_ID { get; set; }

    public int FCD_CM_ID { get; set; }

    public int FCD_FEE_CAT_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FCD_FROM_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FCD_TO_DATE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FCD_AMOUNT { get; set; }

    public bool FCD_IS_ACTIVE { get; set; }

    public int? FCD_SCH_ID { get; set; }

    public int? FCD_CMP_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FCD_SESSION { get; set; }

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
}
