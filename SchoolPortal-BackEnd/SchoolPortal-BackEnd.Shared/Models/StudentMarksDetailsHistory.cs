using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentMarksDetailsHistory")]
public partial class StudentMarksDetailsHistory
{
    [Key]
    public int STUMARKSH_ID { get; set; }

    public int STUMARKSH_STUMARKS_ID { get; set; }

    public Guid STUMARKSH_STU_GUID { get; set; }

    public int STUMARKSH_SUB_CAT_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_Q1_MARKS1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_Q1_MARKS2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_Q2_MARKS1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_Q2_MARKS2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_Q3_MARKS1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_Q3_MARKS2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_FA1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_FA2 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_FA3 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_FA4 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_SA1 { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUMARKSH_SA2 { get; set; }

    public int STUMARKSH_CM_ID { get; set; }

    public int STUMARKSH_SEC_ID { get; set; }

    public int STUMARKSH_CMP_ID { get; set; }

    public int STUMARKSH_SCH_ID { get; set; }

    public bool? STUMARKSH_IS_ACTIVE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUMARKSH_SESSION { get; set; }

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
