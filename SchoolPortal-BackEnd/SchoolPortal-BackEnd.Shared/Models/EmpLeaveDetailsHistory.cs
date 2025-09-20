using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpLeaveDetailsHistory")]
public partial class EmpLeaveDetailsHistory
{
    [Key]
    public int EMPLDH_ID { get; set; }

    public int EMPLDH_EMP_ID { get; set; }

    public int EMPLDH_CAT_ID { get; set; }

    public int EMPLDH_LEAVE_TYPE_ID { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? EMPLDH_TOTAL_LEAVES { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? EMPLDH_PREV_YEAR_BALANCE { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? EMPLDH_CURRENT_BALANCE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMPLDH_SESSION { get; set; }

    public int EMPLDH_CMP_ID { get; set; }

    public int EMPLDH_SCH_ID { get; set; }

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

    [ForeignKey("EMPLDH_CAT_ID")]
    [InverseProperty("EmpLeaveDetailsHistories")]
    public virtual EmpCategoryMaster EMPLDH_CAT { get; set; }

    [ForeignKey("EMPLDH_EMP_ID")]
    [InverseProperty("EmpLeaveDetailsHistories")]
    public virtual EmpMaster EMPLDH_EMP { get; set; }
}
