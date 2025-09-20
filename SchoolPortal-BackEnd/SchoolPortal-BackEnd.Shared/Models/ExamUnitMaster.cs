using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ExamUnitMaster")]
public partial class ExamUnitMaster
{
    [Key]
    public int EXAM_UNIT_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EXAM_UNIT_NAME { get; set; }

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

    [InverseProperty("SCOHUD_UNIT")]
    public virtual ICollection<ScholasticUnitDetail> ScholasticUnitDetails { get; set; } = new List<ScholasticUnitDetail>();
}
