using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class SalaryDesigGradeDetail
{
    [Key]
    public int SALDGD_ID { get; set; }

    public int SALDGD_DESGRADE_ID { get; set; }

    public int SALDGD_SALHM_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SALDGD_VALUE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SALDGD_SESSION { get; set; }

    public bool? SALDGD_IS_ACTIVE { get; set; }

    public int SALDGD_CMP_ID { get; set; }

    public int SALDGD_SCH_ID { get; set; }

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

    [ForeignKey("SALDGD_SALHM_ID")]
    [InverseProperty("SalaryDesigGradeDetails")]
    public virtual SalaryHeadMaster SALDGD_SALHM { get; set; }
}
