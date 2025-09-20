using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("TimeTableClassPeriodDetailsHistory")]
public partial class TimeTableClassPeriodDetailsHistory
{
    [Key]
    public int TTCLPDH_ID { get; set; }

    public int TTCLPDH_CM_ID { get; set; }

    public int TTCLPDH_SEC_ID { get; set; }

    public int? TTCLPDH_SUB_ID { get; set; }

    public int TTCLPDH_PERIOD_ID { get; set; }

    public int TTCLPDH_DAY_OF_WEEK { get; set; }

    public int TTCLPDH_SESSION_ID { get; set; }

    public int TTCLPDH_CMP_ID { get; set; }

    public int TTCLPDH_SCH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TTCLPDH_SESSION { get; set; }

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
