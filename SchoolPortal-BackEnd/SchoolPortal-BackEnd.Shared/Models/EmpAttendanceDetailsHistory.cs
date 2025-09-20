using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpAttendanceDetailsHistory")]
public partial class EmpAttendanceDetailsHistory
{
    [Key]
    public int EMP_ATTENDH_ID { get; set; }

    public int EMP_ATTENDH_EMP_ID { get; set; }

    public int? EMP_ATTENDH_MONTH { get; set; }

    public int? EMP_ATTENDH_YEAR { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EMP_ATTENDH_DATE { get; set; }

    public bool EMP_ATTENDH_STATUS { get; set; }

    public int? EMP_ATTENDH_LEAVE_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_ATTENDH_TIME { get; set; }

    public int EMP_ATTENDH_CMP_ID { get; set; }

    public int EMP_ATTENDH_SCH_ID { get; set; }

    public bool? EMP_ATTENDH_IS_HALF_DAY { get; set; }

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

    [ForeignKey("EMP_ATTENDH_EMP_ID")]
    [InverseProperty("EmpAttendanceDetailsHistories")]
    public virtual EmpMaster EMP_ATTENDH_EMP { get; set; }
}
