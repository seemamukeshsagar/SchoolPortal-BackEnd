using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ProfessionMaster")]
public partial class ProfessionMaster
{
    [Key]
    public int PROF_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PROF_NAME { get; set; }

    public bool? PROF_IS_ACTIVE { get; set; }

    [Required]
    [StringLength(256)]
    public string CREATED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CREATED_DATE { get; set; }

    [Required]
    [StringLength(256)]
    public string MODIFIED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime MODIFIED_DATE { get; set; }
}
