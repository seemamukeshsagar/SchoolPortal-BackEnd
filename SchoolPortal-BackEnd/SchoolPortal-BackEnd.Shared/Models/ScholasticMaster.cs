using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ScholasticMaster")]
public partial class ScholasticMaster
{
    [Key]
    public int SCHOLASTIC_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SCHOLASTIC_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string SCHOLASTIC_DESC { get; set; }

    public int? SCHOLASTIC_PARENT_ID { get; set; }

    public int? SCHOLASTIC_SUBJECT_ID { get; set; }

    public int SCHOLASTIC_CMP_ID { get; set; }

    public int SCHOLASTIC_SCH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SCHOLASTIC_SESSION { get; set; }

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

    [InverseProperty("SCOHUD_SCHOLASTIC")]
    public virtual ICollection<ScholasticUnitDetail> ScholasticUnitDetails { get; set; } = new List<ScholasticUnitDetail>();
}
