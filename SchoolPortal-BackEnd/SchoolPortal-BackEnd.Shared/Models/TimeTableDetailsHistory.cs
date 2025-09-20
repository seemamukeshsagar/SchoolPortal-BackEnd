using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("TimeTableDetailsHistory")]
public partial class TimeTableDetailsHistory
{
    [Key]
    public int TTDETAILH_ID { get; set; }

    public int TTDETAILH_PERIOD_ID { get; set; }

    public int TTDETAILH_TEACHER_ID { get; set; }

    public int TTDETAILH_CM_ID { get; set; }

    public int TTDETAILH_SEC_ID { get; set; }

    public int TTDETAILH_SUB_ID { get; set; }

    public int TTDETAILH_DAY_OF_WEEK { get; set; }

    public int TTDETAILH_CMP_ID { get; set; }

    public int TTDETAILH_SCH_ID { get; set; }

    public int TTDETAILH_SESSION_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TTDETAILH_SESSION { get; set; }

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

    [ForeignKey("TTDETAILH_TEACHER_ID")]
    [InverseProperty("TimeTableDetailsHistories")]
    public virtual EmpMaster TTDETAILH_TEACHER { get; set; }
}
