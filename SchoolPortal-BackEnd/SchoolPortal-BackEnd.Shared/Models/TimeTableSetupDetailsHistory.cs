using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("TimeTableSetupDetailsHistory")]
public partial class TimeTableSetupDetailsHistory
{
    [Key]
    public int TTSETUPH_ID { get; set; }

    [Precision(0)]
    public TimeOnly TTSETUPH_SCHOOL_START_TIME { get; set; }

    [Precision(0)]
    public TimeOnly TTSETUPH_SCHOOL_END_TIME { get; set; }

    [Precision(0)]
    public TimeOnly TTSETUPH_PERIOD_START_TIME { get; set; }

    public int TTSETUPH_TOTAL_PERIODS { get; set; }

    public int TTSETUPH_PERIOD_DURATION { get; set; }

    public int TTSETUPH_RECCESS_DURATION { get; set; }

    public int TTSETUPH_RECCESS_AFTER_PERIOD { get; set; }

    public int TTSETUPH_TTSESSION_ID { get; set; }

    public int TTSETUPH_CMP_ID { get; set; }

    public int TTSETUPH_SCH_ID { get; set; }

    public int? TTSETUPH_FRUIT_RECCESS_DURATION { get; set; }

    public int? TTSETUPH_FRUIT_RECCESS_AFTER_PERIOD { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TTSETUPH_SESSION { get; set; }

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
