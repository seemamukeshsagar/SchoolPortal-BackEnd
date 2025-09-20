using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class TimeTableSubstitutionDetail
{
    [Key]
    public int TTSUBSD_ID { get; set; }

    public int TTSUBSD_PERIOD_ID { get; set; }

    public int TTSUBSD_TEACHER_ID { get; set; }

    public int TTSUBSD_TEACHER_ID_NEW { get; set; }

    public int TTSUBSD_SUB_ID { get; set; }

    public int TTSUBSD_CM_ID { get; set; }

    public int TTSUBSD_SEC_ID { get; set; }

    public int TTSUBSD_DAY_OF_WEEK { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TTSUBSD_SUBSTITUTION_DATE { get; set; }

    public int TTSUBSD_SESSION_ID { get; set; }

    public int TTSUBSD_CMP_ID { get; set; }

    public int TTSUBSD_SCH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TTSUBSD_SESSION { get; set; }

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

    [ForeignKey("TTSUBSD_TEACHER_ID")]
    [InverseProperty("TimeTableSubstitutionDetailTTSUBSD_TEACHERs")]
    public virtual EmpMaster TTSUBSD_TEACHER { get; set; }

    [ForeignKey("TTSUBSD_TEACHER_ID_NEW")]
    [InverseProperty("TimeTableSubstitutionDetailTTSUBSD_TEACHER_ID_NEWNavigations")]
    public virtual EmpMaster TTSUBSD_TEACHER_ID_NEWNavigation { get; set; }
}
