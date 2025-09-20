using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class StudentAchievement
{
    [Key]
    public int STUD_ACH_ID { get; set; }

    public Guid STUD_STU_GUID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUD_ACH_SCHOOL { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string STUD_ACH_SESSION { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STUD_ACH_DESCRIPTION { get; set; }

    public int STUD_SCH_ID { get; set; }

    public int STUD_CMP_ID { get; set; }

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
