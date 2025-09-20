using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpCatLeaveDetailsHistory")]
public partial class EmpCatLeaveDetailsHistory
{
    [Key]
    public int ECATH_LEAVE_DETAIL_ID { get; set; }

    public int? ECATH_LEAVE_CAT_ID { get; set; }

    public int? ECATH_LEAVE_TYPE_ID { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? ECATH_LEAVE_TOTAL_LEAVES { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ECATH_LEAVE_SESSION { get; set; }

    public int ECATH_LEAVE_SCH_ID { get; set; }

    public int ECATH_LEAVE_CMP_ID { get; set; }

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

    [ForeignKey("ECATH_LEAVE_CAT_ID")]
    [InverseProperty("EmpCatLeaveDetailsHistories")]
    public virtual EmpCategoryMaster ECATH_LEAVE_CAT { get; set; }
}
