using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class BillDetail
{
    [Key]
    public int BILL_DETAIL_ID { get; set; }

    public int BILL_DETAIL_BILL_ID { get; set; }

    public int? BILL_DETAIL_EXP_CAT_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string BILL_DETAIL_DESC { get; set; }

    public int? BILL_DETAIL_QTY { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? BILL_DETAIL_AMOUNT { get; set; }

    public int BILL_CMP_ID { get; set; }

    public int BILL_SCH_ID { get; set; }

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
