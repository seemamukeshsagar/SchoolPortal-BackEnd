using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class EmpLeaveDetail
{
    [Key]
    public int EMPLD_ID { get; set; }

    public int EMPLD_EMP_ID { get; set; }

    public int EMPLD_CAT_ID { get; set; }

    public int EMPLD_LEAVE_TYPE_ID { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? EMPLD_TOTAL_LEAVES { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? EMPLD_PREV_YEAR_BALANCE { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? EMPLD_CURRENT_BALANCE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMPLD_SESSION { get; set; }

    public int EMPLD_CMP_ID { get; set; }

    public int EMPLD_SCH_ID { get; set; }

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

    [ForeignKey("EMPLD_CAT_ID")]
    [InverseProperty("EmpLeaveDetails")]
    public virtual EmpCategoryMaster EMPLD_CAT { get; set; }

    [ForeignKey("EMPLD_EMP_ID")]
    [InverseProperty("EmpLeaveDetails")]
    public virtual EmpMaster EMPLD_EMP { get; set; }
}
