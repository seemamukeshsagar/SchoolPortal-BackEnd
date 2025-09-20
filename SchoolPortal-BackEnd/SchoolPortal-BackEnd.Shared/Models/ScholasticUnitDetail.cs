using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[PrimaryKey("SCOHUD_SCHOLASTIC_ID", "SCOHUD_UNIT_ID")]
[Table("ScholasticUnitDetail")]
public partial class ScholasticUnitDetail
{
    [Key]
    public int SCOHUD_SCHOLASTIC_ID { get; set; }

    [Key]
    public int SCOHUD_UNIT_ID { get; set; }

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

    [ForeignKey("SCOHUD_SCHOLASTIC_ID")]
    [InverseProperty("ScholasticUnitDetails")]
    public virtual ScholasticMaster SCOHUD_SCHOLASTIC { get; set; }

    [ForeignKey("SCOHUD_UNIT_ID")]
    [InverseProperty("ScholasticUnitDetails")]
    public virtual ExamUnitMaster SCOHUD_UNIT { get; set; }
}
