using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class EmpDocumentDetail
{
    [Key]
    public int EDOC_ID { get; set; }

    public int EDOC_EMP_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EDOC_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string EDOC_DESCRIPTION { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EDOC_FILENAME { get; set; }

    public int EDOC_CMP_ID { get; set; }

    public int EDOC_SCH_ID { get; set; }

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

    [ForeignKey("EDOC_EMP_ID")]
    [InverseProperty("EmpDocumentDetails")]
    public virtual EmpMaster EDOC_EMP { get; set; }
}
