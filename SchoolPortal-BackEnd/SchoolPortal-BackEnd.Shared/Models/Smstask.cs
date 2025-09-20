using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SMSTask")]
public partial class SMSTask
{
    [Key]
    public int STS_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STS_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STS_DESCRIPTION { get; set; }

    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string STS_NOTIFICATION_SEND_EMAIL { get; set; }

    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string STS_NOTIFICATION_SEND_SMS { get; set; }

    public int STS_NOTIFICATION_RECEIEVER_ID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string STS_REPEAT_SCHEDULE { get; set; }

    public int STS_STATUS_ID { get; set; }

    public bool STS_IS_ACTIVE { get; set; }

    public int STS_CMP_ID { get; set; }

    public int STS_SCH_ID { get; set; }

    public bool? STS_IS_READONLY { get; set; }

    public int? STS_LAST_RUN_STATUS_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? STS_LAST_RUN_DATE { get; set; }

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

    [InverseProperty("STS_SCHEDULE_TASK")]
    public virtual ICollection<SMSTaskSchedule> SMSTaskSchedules { get; set; } = new List<SMSTaskSchedule>();

    [InverseProperty("STSSMTP_TASK")]
    public virtual ICollection<SMSTaskSmtpDetail> SMSTaskSmtpDetails { get; set; } = new List<SMSTaskSmtpDetail>();

    [ForeignKey("STS_NOTIFICATION_RECEIEVER_ID")]
    [InverseProperty("SMSTasks")]
    public virtual NotificationReceiverMaster STS_NOTIFICATION_RECEIEVER { get; set; }
}
