using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SMSTaskHistory")]
public partial class SMSTaskHistory
{
    [Key]
    public int STSH_ID { get; set; }

    public int STSH_TASK_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime STSH_SENT_DATE { get; set; }

    public int STSH_NOTIFICATION_RECEIVER_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STSH_NOTIFICATION_RECEIVER { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STSH_SEND_TYPE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STSH_STATUS { get; set; }

    public Guid? STSH_STU_GUID { get; set; }

    public int? STSH_PARENT_ID { get; set; }

    public int? STSH_TEACHER_ID { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STSH_EMAIL { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STSH_PHONE { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string STSH_DESCRIPTION { get; set; }

    public int STSH_CMP_ID { get; set; }

    public int STSH_SCH_ID { get; set; }

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
