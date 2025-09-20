using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentReportCardDetailsHistory")]
public partial class StudentReportCardDetailsHistory
{
    [Key]
    public int REPCARDH_DETAIL_ID { get; set; }

    public int REPCARDH_DETAIL_REPCARD_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string REPCARDH_DETAIL_SUBJECT_TYPE { get; set; }

    public int? REPCARDH_DETAIL_SUBJECT_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_QUARTER_MARKS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_FA1_MARKS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_FA2_MARKS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_SA_MARKS { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string REPCARDH_DETAIL_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string REPCARDH_DETAIL_FA1_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string REPCARDH_DETAIL_FA2_GRADE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string REPCARDH_DETAIL_SA_GRADE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_TOTAL_MARKS { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string REPCARDH_DETAIL_TOTAL_GRADE { get; set; }

    public int? REPCARDH_DETAIL_CMP_ID { get; set; }

    public int? REPCARDH_DETAIL_SCH_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_TOTAL_MARKS_QUARTER { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_TOTAL_MARKS_FA1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_TOTAL_MARKS_FA2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_TOTAL_MARKS_SA { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? REPCARDH_DETAIL_TOTAL_MARKS_FINAL { get; set; }

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
