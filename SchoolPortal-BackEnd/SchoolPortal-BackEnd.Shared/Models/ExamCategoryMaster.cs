using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ExamCategoryMaster")]
public partial class ExamCategoryMaster
{
    [Key]
    public int EXAM_CAT_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EXAM_CAT_NAME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EXAM_CAT_DESC { get; set; }

    public int EXAM_CAT_CMP_ID { get; set; }

    public int EXAM_CAT_SCH_ID { get; set; }

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
