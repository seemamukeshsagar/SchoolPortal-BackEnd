using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SalaryDesigGradeDetailsHistory")]
public partial class SalaryDesigGradeDetailsHistory
{
    [Key]
    public int SALDGDH_ID { get; set; }

    public int SALDGDH_DESGRADE_ID { get; set; }

    public int SALDGDH_SALHM_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SALDGDH_VALUE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SALDGDH_SESSION { get; set; }

    public bool? SALDGDH_IS_ACTIVE { get; set; }

    public int SALDGDH_CMP_ID { get; set; }

    public int SALDGDH_SCH_ID { get; set; }

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

    [ForeignKey("SALDGDH_SALHM_ID")]
    [InverseProperty("SalaryDesigGradeDetailsHistories")]
    public virtual SalaryHeadMaster SALDGDH_SALHM { get; set; }
}
