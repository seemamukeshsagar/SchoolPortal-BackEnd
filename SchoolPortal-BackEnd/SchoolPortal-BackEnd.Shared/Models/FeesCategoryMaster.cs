using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("FeesCategoryMaster")]
public partial class FeesCategoryMaster
{
    [Key]
    public int FCM_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FCM_CAT_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string FCM_CAT_DESCRIPTION { get; set; }

    public bool? FCM_CAT_IS_ACTIVE { get; set; }

    public int? FCM_CAT_SCH_ID { get; set; }

    public int? FCM_CAT_CMP_ID { get; set; }

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

    [InverseProperty("FCDH_FEE_CAT")]
    public virtual ICollection<FeeClassDetailsHistory> FeeClassDetailsHistories { get; set; } = new List<FeeClassDetailsHistory>();
}
