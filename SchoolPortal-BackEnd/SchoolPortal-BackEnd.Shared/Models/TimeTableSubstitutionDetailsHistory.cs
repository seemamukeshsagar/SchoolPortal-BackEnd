using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("TimeTableSubstitutionDetailsHistory")]
public partial class TimeTableSubstitutionDetailsHistory
{
    [Key]
    public int TTSUBSDH_ID { get; set; }

    public int TTSUBSDH_PERIOD_ID { get; set; }

    public int TTSUBSDH_TEACHER_ID { get; set; }

    public int TTSUBSDH_TEACHER_ID_NEW { get; set; }

    public int TTSUBSDH_SUB_ID { get; set; }

    public int TTSUBSDH_CM_ID { get; set; }

    public int TTSUBSDH_SEC_ID { get; set; }

    public int TTSUBSDH_DAY_OF_WEEK { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TTSUBSDH_SUBSTITUTION_DATE { get; set; }

    public int TTSUBSDH_SESSION_ID { get; set; }

    public int TTSUBSDH_CMP_ID { get; set; }

    public int TTSUBSDH_SCH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TTSUBSDH_SESSION { get; set; }

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

    [ForeignKey("TTSUBSDH_TEACHER_ID")]
    [InverseProperty("TimeTableSubstitutionDetailsHistoryTTSUBSDH_TEACHERs")]
    public virtual EmpMaster TTSUBSDH_TEACHER { get; set; }

    [ForeignKey("TTSUBSDH_TEACHER_ID_NEW")]
    [InverseProperty("TimeTableSubstitutionDetailsHistoryTTSUBSDH_TEACHER_ID_NEWNavigations")]
    public virtual EmpMaster TTSUBSDH_TEACHER_ID_NEWNavigation { get; set; }
}
