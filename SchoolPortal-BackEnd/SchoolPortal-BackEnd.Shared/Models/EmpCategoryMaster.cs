using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpCategoryMaster")]
public partial class EmpCategoryMaster
{
    [Key]
    public int EMP_CAT_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_CAT_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EMP_CAT_DESC { get; set; }

    public int EMP_CAT_CMP_ID { get; set; }

    public int EMP_CAT_SCH_ID { get; set; }

    public bool EMP_CAT_IS_ACTIVE { get; set; }

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

    [InverseProperty("ECAT_LEAVE_CAT")]
    public virtual ICollection<EmpCatLeaveDetail> EmpCatLeaveDetails { get; set; } = new List<EmpCatLeaveDetail>();

    [InverseProperty("ECATH_LEAVE_CAT")]
    public virtual ICollection<EmpCatLeaveDetailsHistory> EmpCatLeaveDetailsHistories { get; set; } = new List<EmpCatLeaveDetailsHistory>();

    [InverseProperty("EMPLD_CAT")]
    public virtual ICollection<EmpLeaveDetail> EmpLeaveDetails { get; set; } = new List<EmpLeaveDetail>();

    [InverseProperty("EMPLDH_CAT")]
    public virtual ICollection<EmpLeaveDetailsHistory> EmpLeaveDetailsHistories { get; set; } = new List<EmpLeaveDetailsHistory>();

    [InverseProperty("EMP_CAT")]
    public virtual ICollection<EmpMaster> EmpMasters { get; set; } = new List<EmpMaster>();
}
