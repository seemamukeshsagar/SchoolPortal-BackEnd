using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class SMSTaskSmtpDetail
{
    [Key]
    public int STSSMTP_DETAIL_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_FROM_ADDRESS { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_GATEWAY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_USERNAME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_PASSWORD { get; set; }

    [StringLength(6000)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_SUBJECT { get; set; }

    [StringLength(6000)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_BODY_TEXT { get; set; }

    public int? STSSMTP_TASK_ID { get; set; }

    public bool? STSSMTP_DETAIL_IS_ACTIVE { get; set; }

    public int STSSMTP_DETAIL_CMP_ID { get; set; }

    public int STSSMTP_DETAIL_SCH_ID { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_MESSAGE_SUBJECT { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string STSSMTP_DETAIL_MESSAGE_BODY { get; set; }

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

    [ForeignKey("STSSMTP_TASK_ID")]
    [InverseProperty("SMSTaskSmtpDetails")]
    public virtual SMSTask STSSMTP_TASK { get; set; }
}
