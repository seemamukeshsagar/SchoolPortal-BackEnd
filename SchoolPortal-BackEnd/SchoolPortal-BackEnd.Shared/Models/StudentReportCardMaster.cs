using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentReportCardMaster")]
public partial class StudentReportCardMaster
{
    [Key]
    public int REPCARD_ID { get; set; }

    public Guid REPCARD_STU_GUID { get; set; }

    public int REPCARD_CM_ID { get; set; }

    public int REPCARD_SEC_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string REPCARD_SESSION { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string REPCARD_TYPE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string REPCARD_TYPE_VALUE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? REPCARD_PERIOD { get; set; }

    public int? REPCARD_CMP_ID { get; set; }

    public int? REPCARD_SCH_ID { get; set; }

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
