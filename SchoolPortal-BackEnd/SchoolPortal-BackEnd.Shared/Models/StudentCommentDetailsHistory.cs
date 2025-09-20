using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentCommentDetailsHistory")]
public partial class StudentCommentDetailsHistory
{
    [Key]
    public int STUCOMMENTH_ID { get; set; }

    public int STUCOMMENTH_STUCOMMENT_ID { get; set; }

    public Guid STUCOMMENTH_STU_GUID { get; set; }

    public int STUCOMMENTH_CM_ID { get; set; }

    public int STUCOMMENTH_SEC_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUCOMMENTH_SESSION { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string STUCOMMENTH_Q1_DESC { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string STUCOMMENTH_Q2_DESC { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string STUCOMMENTH_Q3_DESC { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string STUCOMMENTH_SA1_DESC { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string STUCOMMENTH_SA2_DESC { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string STUCOMMENTH_FINAL_DESC { get; set; }

    public bool? STUCOMMENTH_IS_AACTIVE { get; set; }

    public int STUCOMMENTH_CMP_ID { get; set; }

    public int STUCOMMENTH_SCH_ID { get; set; }

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
