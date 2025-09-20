using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SMSTaskStatusMaster")]
public partial class SMSTaskStatusMaster
{
    [Key]
    public int STSSM_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STSSM_NAME { get; set; }

    public bool? STSSM_IS_ACTIVE { get; set; }

    public int STSSM_CMP_ID { get; set; }

    public int STSSM_SCH_ID { get; set; }

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
