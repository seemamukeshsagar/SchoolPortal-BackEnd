using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpSalaryStructureDetailsHistory")]
public partial class EmpSalaryStructureDetailsHistory
{
    [Key]
    public int ESSDH_ID { get; set; }

    public int ESSDH_EMP_ID { get; set; }

    public int ESSDH_SALHM_ID { get; set; }

    public int ESSDH_DESGRADE_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ESSDH_SESSION { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESSDH_VALUE { get; set; }

    public bool ESSDH_IS_ACTIVE { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string ESSDH_SALARY_TYPE { get; set; }

    public bool ESSDH_IS_DEDUCTANCE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ESSDH_SALH_CODE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ESDDH_SALH_DESCRIPTION { get; set; }

    public bool? ESDDH_SALH_IS_READONLY { get; set; }

    public int ESDDH_CMP_ID { get; set; }

    public int ESDDH_SCH_ID { get; set; }

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

    [ForeignKey("ESSDH_EMP_ID")]
    [InverseProperty("EmpSalaryStructureDetailsHistories")]
    public virtual EmpMaster ESSDH_EMP { get; set; }
}
