using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class EmpSalaryDetail
{
    [Key]
    public int ESALD_ID { get; set; }

    public int ESALD_ESALM_ID { get; set; }

    public int? ESALD_SALHM_ID { get; set; }

    public int? ESALD_DESGRADE_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALD_VALUE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ESALD_SALARY_TYPE { get; set; }

    public bool ESALD_IS_DEDUCTION { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ESALD_SAL_CODE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ESALD_SAL_DESCRIPTION { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALD_AMOUNT { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ESALD_IS_SALARY_HEAD { get; set; }

    public int ESALD_CMP_ID { get; set; }

    public int ESALD_SCH_ID { get; set; }

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
