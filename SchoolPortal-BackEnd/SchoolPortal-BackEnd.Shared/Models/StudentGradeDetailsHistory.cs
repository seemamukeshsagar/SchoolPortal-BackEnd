using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentGradeDetailsHistory")]
public partial class StudentGradeDetailsHistory
{
    [Key]
    public int STUGRADEH_ID { get; set; }

    public int STUGRADEH_STUGRADE_ID { get; set; }

    public Guid STUGRADEH_STU_GUID { get; set; }

    public int STUGRADEH_SCHOLASTIC_CAT_ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_Q1_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_Q2_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_Q3_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_FA1_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_FA2_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_FA3_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_FA4_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_SA1_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUGRADEH_SA2_GRADE { get; set; }

    public int STUGRADEH_CM_ID { get; set; }

    public int STUGRADEH_SEC_ID { get; set; }

    public int STUGRADEH_CMP_ID { get; set; }

    public int STUGRADEH_SCH_ID { get; set; }

    public bool? STUGRADEH_IS_ACTIVE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUGRADEH_SESSION { get; set; }

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
