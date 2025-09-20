using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class TimeTableClassPeriodDetail
{
    [Key]
    public int TTCLPD_ID { get; set; }

    public int TTCLPD_CM_ID { get; set; }

    public int TTCLPD_SEC_ID { get; set; }

    public int? TTCLPD_SUB_ID { get; set; }

    public int TTCLPD_PERIOD_ID { get; set; }

    public int TTCLPD_DAY_OF_WEEK { get; set; }

    public int TTCLPD_SESSION_ID { get; set; }

    public int TTCLPD_CMP_ID { get; set; }

    public int TTCLPD_SCH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TTCLPD_SESSION { get; set; }

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
