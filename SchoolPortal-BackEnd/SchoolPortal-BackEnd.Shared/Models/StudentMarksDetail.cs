using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class StudentMarksDetail
{
    [Key]
    public int STUMARKS_ID { get; set; }

    public Guid STUMARKS_STU_GUID { get; set; }

    public int STUMARKS_SUB_CAT_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_Q1_MARKS1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_Q1_MARKS2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_Q2_MARKS1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_Q2_MARKS2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_Q3_MARKS1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_Q3_MARKS2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_FA1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_FA2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_FA3 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_FA4 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_SA1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKS_SA2 { get; set; }

    public int STUMARKS_CM_ID { get; set; }

    public int STUMARKS_SEC_ID { get; set; }

    public int STUMARKS_CMP_ID { get; set; }

    public int STUMARKS_SCH_ID { get; set; }

    public bool? STUMARKS_IS_ACTIVE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUMARKS_SESSION { get; set; }

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
