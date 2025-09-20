using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpTypeMaster")]
public partial class EmpTypeMaster
{
    [Key]
    public int EMP_TYPE_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_TYPE_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EMP_TYPE_DESC { get; set; }

    public int EMP_TYPE_CMP_ID { get; set; }

    public int EMP_TYPE_SCH_ID { get; set; }

    public bool EMP_TYPE_IS_ACTIVE { get; set; }

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

    [InverseProperty("EMP_TYPE")]
    public virtual ICollection<EmpMaster> EmpMasters { get; set; } = new List<EmpMaster>();
}
