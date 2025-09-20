using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpSalaryMasterHistory")]
public partial class EmpSalaryMasterHistory
{
    [Key]
    public int ESALMH_ID { get; set; }

    public int ESALMH_ESALM_ID { get; set; }

    public int ESALMH_EMP_ID { get; set; }

    public int ESALMH_MONTH { get; set; }

    public int ESALMH_YEAR { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ESALMH_SESSION { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ESALMH_BATCHPRINT_DATE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALMH_BASIC_SALARY { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALMH_ALLOWANCES { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALMH_DEDUCTIONS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALMH_NET_SALARY { get; set; }

    public int ESALMH_TOTAL_DAYS { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? ESALMH_PRESENT_DAYS { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? ESALMH_ABSENT_DAYS { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? ESALMH_LEAVE_DAYS { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ESALMH_LEAVE_DESC { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ESALMH_LEAVE_BALANCE_DESC { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALMH_PER_DAY_SALARY { get; set; }

    public int? ESALMH_DEPT_ID { get; set; }

    public int? ESALMH_DESIG_ID { get; set; }

    public int? ESALMH_GRADE_ID { get; set; }

    public int ESALMH_CMP_ID { get; set; }

    public int ESALMH_SCH_ID { get; set; }

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
