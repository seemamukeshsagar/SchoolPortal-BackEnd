using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[PrimaryKey("EPQUALD_EMP_ID", "EPQUALD_QUAL_ID")]
public partial class EmpProfQualiDetail
{
    [Key]
    public int EPQUALD_EMP_ID { get; set; }

    [Key]
    public int EPQUALD_QUAL_ID { get; set; }

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

    [ForeignKey("EPQUALD_EMP_ID")]
    [InverseProperty("EmpProfQualiDetails")]
    public virtual EmpMaster EPQUALD_EMP { get; set; }
}
