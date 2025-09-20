using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SalaryHeadMaster")]
public partial class SalaryHeadMaster
{
    [Key]
    public int SALHM_ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string SALHM_CODE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SALHM_DESCRIPTION { get; set; }

    public bool? SALHM_IS_READONLY { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string SALHM_TYPE { get; set; }

    public bool SALHM_IS_DEDUCTION { get; set; }

    public bool SALHM_IS_ACTIVE { get; set; }

    public int SALHM_CMP_ID { get; set; }

    public int SALHM_SCH_ID { get; set; }

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

    [InverseProperty("SALDGD_SALHM")]
    public virtual ICollection<SalaryDesigGradeDetail> SalaryDesigGradeDetails { get; set; } = new List<SalaryDesigGradeDetail>();

    [InverseProperty("SALDGDH_SALHM")]
    public virtual ICollection<SalaryDesigGradeDetailsHistory> SalaryDesigGradeDetailsHistories { get; set; } = new List<SalaryDesigGradeDetailsHistory>();
}
