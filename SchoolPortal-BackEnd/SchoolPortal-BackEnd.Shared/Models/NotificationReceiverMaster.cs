using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("NotificationReceiverMaster")]
public partial class NotificationReceiverMaster
{
    [Key]
    public int NOTIFICATION_RECEIVER_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NOTIFICATION_RECEIVER_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string NOTIFICATION_RECEIVER_DESC { get; set; }

    public int NOTIFICATION_RECEIVER_CMP_ID { get; set; }

    public int? NOTIFICATION_RECEIVER_SCH_ID { get; set; }

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

    [InverseProperty("STS_NOTIFICATION_RECEIEVER")]
    public virtual ICollection<SMSTask> SMSTasks { get; set; } = new List<SMSTask>();
}
