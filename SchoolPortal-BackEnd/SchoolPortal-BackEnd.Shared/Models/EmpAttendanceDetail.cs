using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class EmpAttendanceDetail
{
    [Key]
    public int EMP_ATTEND_ID { get; set; }

    public int EMP_ATTEND_EMP_ID { get; set; }

    public int? EMP_ATTEND_MONTH { get; set; }

    public int? EMP_ATTEND_YEAR { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EMP_ATTEND_DATE { get; set; }

    public bool EMP_ATTEND_STATUS { get; set; }

    public int? EMP_ATTEND_LEAVE_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_ATTEND_TIME { get; set; }

    public int EMP_ATTEND_CMP_ID { get; set; }

    public int EMP_ATTEND_SCH_ID { get; set; }

    public bool? EMP_ATTEND_IS_HALF_DAY { get; set; }

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

    [ForeignKey("EMP_ATTEND_EMP_ID")]
    [InverseProperty("EmpAttendanceDetails")]
    public virtual EmpMaster EMP_ATTEND_EMP { get; set; }
}
