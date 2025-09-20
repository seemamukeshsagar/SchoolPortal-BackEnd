using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class SystemParameter
{
    [Key]
    public int SYS_PARAMETER_ID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string SYSP_PARAMETER_NAME { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string SYSP_PARAMETER_VALUE { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string SYSP_PARAMETER_DESCRIPTION { get; set; }

    public int SYSP_CMP_ID { get; set; }

    public int SYSP_SCH_ID { get; set; }

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
