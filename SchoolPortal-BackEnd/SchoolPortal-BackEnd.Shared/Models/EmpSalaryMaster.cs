using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpSalaryMaster")]
public partial class EmpSalaryMaster
{
    [Key]
    public int ESALM_ID { get; set; }

    public int ESALM_EMP_ID { get; set; }

    public int ESALM_MONTH { get; set; }

    public int ESALM_YEAR { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ESALM_SESSION { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ESALM_BATCHPRINT_DATE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALM_BASIC_SALARY { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALM_ALLOWANCES { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALM_DEDUCTIONS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALM_NET_SALARY { get; set; }

    public int ESALM_TOTAL_DAYS { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? ESALM_PRESENT_DAYS { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? ESALM_ABSENT_DAYS { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? ESALM_LEAVE_DAYS { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ESALM_LEAVE_DESC { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string ESALM_LEAVE_BALANCE_DESC { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ESALM_PER_DAY_SALARY { get; set; }

    public int? ESALM_DEPT_ID { get; set; }

    public int? ESALM_DESIG_ID { get; set; }

    public int? ESALM_GRADE_ID { get; set; }

    public int ESALM_CMP_ID { get; set; }

    public int ESALM_SCH_ID { get; set; }

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
