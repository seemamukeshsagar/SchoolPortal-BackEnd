using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("TimeTablePeriodMasterHistory")]
public partial class TimeTablePeriodMasterHistory
{
    [Key]
    public int TTPERIODH_ID { get; set; }

    public int TTPERIODH_PERIOD_ID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string TTPERIODH_DESCTIPTION { get; set; }

    public int TTPERIODH_NUMBER { get; set; }

    [Precision(0)]
    public TimeOnly TTPERIODH_START_TIME { get; set; }

    [Precision(0)]
    public TimeOnly TTPERIODH_END_TIME { get; set; }

    public int TTPERIODH_TTSESSION_ID { get; set; }

    public int TTPERIODH_CMP_ID { get; set; }

    public int TTPERIODH_SCH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TTPERIODH_SESSION { get; set; }

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
