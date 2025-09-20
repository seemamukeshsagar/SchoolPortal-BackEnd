using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("MarksGradeMaster")]
public partial class MarksGradeMaster
{
    [Key]
    public int MARKS_GRADE_ID { get; set; }

    public int? MARKS_GRADE_CLASS_ID { get; set; }

    public int? MARKS_GRADE_MIN_RANGE { get; set; }

    public int? MARKS_GRADE_MAX_RANGE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string MARKS_GRADE_CODE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MARKS_GRADE_POINT { get; set; }

    public int MARKS_GRADE_CMP_ID { get; set; }

    public int MARKS_GRADE_SCH_ID { get; set; }

    public bool? MARKS_GRADE_IS_ACTIVE { get; set; }

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

    public DateOnly MODIFIED_DATE { get; set; }
}
