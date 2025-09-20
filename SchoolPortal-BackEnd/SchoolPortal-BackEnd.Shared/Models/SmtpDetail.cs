using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class SmtpDetail
{
    [Key]
    public int SMTP_DETAIL_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SMTP_DETAIL_FROM_ADDRESS { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SMTP_DETAIL_GATEWAY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SMTP_DETAIL_USERNAME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SMTP_DETAIL_PASSWORD { get; set; }

    [StringLength(6000)]
    [Unicode(false)]
    public string SMTP_DETAIL_SUBJECT { get; set; }

    [StringLength(6000)]
    [Unicode(false)]
    public string SMTP_DETAIL_BODY_TEXT { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SMTP_DETAIL_EMAIL_TYPE { get; set; }

    public bool? SMTP_DETAIL_IS_ACTIVE { get; set; }

    public int SMTP_DETAIL_CMP_ID { get; set; }

    public int SMTP_DETAIL_SCH_ID { get; set; }

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
