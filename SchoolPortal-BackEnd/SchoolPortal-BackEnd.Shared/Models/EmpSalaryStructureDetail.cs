using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class EmpSalaryStructureDetail
{
    [Key]
    public int ESSD_ID { get; set; }

    public int ESSD_EMP_ID { get; set; }

    public int ESSD_SALHM_ID { get; set; }

    public int ESSD_DESGRADE_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ESSD_SESSION { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESSD_VALUE { get; set; }

    public bool ESSD_IS_ACTIVE { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string ESSD_SALARY_TYPE { get; set; }

    public bool ESSD_IS_DEDUCTANCE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ESSD_SALH_CODE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ESDD_SALH_DESCRIPTION { get; set; }

    public bool? ESDD_SALH_IS_READONLY { get; set; }

    public int ESDD_CMP_ID { get; set; }

    public int ESDD_SCH_ID { get; set; }

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

    [ForeignKey("ESSD_EMP_ID")]
    [InverseProperty("EmpSalaryStructureDetails")]
    public virtual EmpMaster ESSD_EMP { get; set; }
}
