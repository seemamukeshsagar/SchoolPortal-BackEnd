using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentFeeDetailsHistory")]
public partial class StudentFeeDetailsHistory
{
    [Key]
    public int STUDFEEH_ID { get; set; }

    public Guid STUDFEEH_STU_GUID { get; set; }

    public int STUDFEEH_CM_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime STUD_FEEH_DUE_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? STUD_FEEH_DATE { get; set; }

    public bool STUD_FEESH_PAID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUD_FEEH_AMOUNT { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUD_LATE_FEEH_AMOUNT { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUD_FEEH_TOTAL_AMOUNT { get; set; }

    public long? STUD_FEEH_IS_ACTIVE { get; set; }

    public int STUD_FEEH_MONTH { get; set; }

    public int STUD_FEEH_YEAR { get; set; }

    public int STUD_SCH_ID { get; set; }

    public int STUD_CMP_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUD_FEEH_RECEIPT_NUMBER { get; set; }

    public int? STUD_FEEH_CAT_ID { get; set; }

    public int? STUDFEEH_SEC_ID { get; set; }

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
