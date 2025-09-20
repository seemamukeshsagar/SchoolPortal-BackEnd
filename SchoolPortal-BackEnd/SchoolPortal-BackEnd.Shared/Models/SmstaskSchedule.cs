using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SMSTaskSchedule")]
public partial class SMSTaskSchedule
{
    [Key]
    public int STS_SCHEDULE_ID { get; set; }

    public int STS_SCHEDULE_TASK_ID { get; set; }

    public bool STS_SCHEDULE_SUNDAY { get; set; }

    public bool STS_SCHEDULE_MONDAY { get; set; }

    public bool STS_SCHEDULE_TUESDAY { get; set; }

    public bool STS_SCHEDULE_WEDNESDAY { get; set; }

    public bool STS_SCHEDULE_THRUSDAY { get; set; }

    public bool STS_SCHEDULE_FRIDAY { get; set; }

    public bool STS_SCHEDULE_SATURDAY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? STS_SCHEDULE_STARTTIME { get; set; }

    public int STS_CMP_ID { get; set; }

    public int STS_SCH_ID { get; set; }

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

    [ForeignKey("STS_SCHEDULE_TASK_ID")]
    [InverseProperty("SMSTaskSchedules")]
    public virtual SMSTask STS_SCHEDULE_TASK { get; set; }
}
