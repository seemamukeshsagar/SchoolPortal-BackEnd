using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("VoucherMaster")]
public partial class VoucherMaster
{
    [Key]
    public int VOUCHER_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VOUCHER_NUMBER { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string VOUCHER_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string VOUCHER_DESCRIPTION { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime VOUCHER_DATE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? VOUCHER_AMOUNT { get; set; }

    public int VOUCHER_EXPENSE_CATEGORY_ID { get; set; }

    public bool? VOUCHER_IS_ACTIVE { get; set; }

    public int VOUCHER_CMP_ID { get; set; }

    public int VOUCHER_SCH_ID { get; set; }

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
