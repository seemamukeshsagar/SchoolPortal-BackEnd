using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("VisitorMaster")]
public partial class VisitorMaster
{
    [Key]
    public int VM_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VM_NUMBER { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string VM_NAME { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime VM_DATE { get; set; }

    [Precision(0)]
    public TimeOnly VM_TIME_OF_ARRIVAL { get; set; }

    [Precision(0)]
    public TimeOnly VM_TIME_OF_EXIT { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string VM_PURPOSE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string VM_CONTACT_PERSON { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string VM_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VM_CONTACT_NUMBER { get; set; }

    public bool? VM_IS_ACTIVE { get; set; }

    public int VM_CMP_ID { get; set; }

    public int VM_SCH_ID { get; set; }

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
