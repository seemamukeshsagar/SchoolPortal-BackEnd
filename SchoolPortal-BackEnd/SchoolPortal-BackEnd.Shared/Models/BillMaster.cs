using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("BillMaster")]
public partial class BillMaster
{
    [Key]
    public int BILL_ID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string BILL_NUMBER { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime BILL_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BILL_DUE_DATE { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string BILL_DESC { get; set; }

    public int BILL_VENDOR_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? BILL_AMOUNT { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? BILL_TAX_AMOUNT { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? BILL_TOTAL_AMOUNT { get; set; }

    public int BILL_CMP_ID { get; set; }

    public int BILL_SCH_ID { get; set; }

    public int? BILL_STATUS { get; set; }

    public int? BILL_VEHICLE_ID { get; set; }

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

    [ForeignKey("BILL_VEHICLE_ID")]
    [InverseProperty("BillMasters")]
    public virtual VehicleMaster BILL_VEHICLE { get; set; }

    [ForeignKey("BILL_VENDOR_ID")]
    [InverseProperty("BillMasters")]
    public virtual VendorMaster BILL_VENDOR { get; set; }
}
