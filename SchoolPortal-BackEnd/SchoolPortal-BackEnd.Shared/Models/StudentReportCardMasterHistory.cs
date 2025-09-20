using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentReportCardMasterHistory")]
public partial class StudentReportCardMasterHistory
{
    [Key]
    public int REPCARDH_ID { get; set; }

    public int REPCARDH_REPCARD_ID { get; set; }

    public Guid REPCARDH_STU_GUID { get; set; }

    public int REPCARDH_CM_ID { get; set; }

    public int REPCARDH_SEC_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string REPCARDH_SESSION { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string REPCARDH_TYPE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string REPCARDH_TYPE_VALUE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? REPCARDH_PERIOD { get; set; }

    public int? REPCARDH_CMP_ID { get; set; }

    public int? REPCARDH_SCH_ID { get; set; }

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
