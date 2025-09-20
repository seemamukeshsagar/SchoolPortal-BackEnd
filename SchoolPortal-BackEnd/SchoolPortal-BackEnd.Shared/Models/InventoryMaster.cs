using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("InventoryMaster")]
public partial class InventoryMaster
{
    [Key]
    public int INV_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string INV_NAME { get; set; }

    public int INV_ITEM_ID { get; set; }

    public int INV_ITEM_LOCATION_ID { get; set; }

    public int? INV_QUANTITY { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? INV_COST_PER_ITEM { get; set; }

    public bool? INV_IS_ACTIVE { get; set; }

    public int INV_CMP_ID { get; set; }

    public int INV_SCH_ID { get; set; }

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

    [ForeignKey("INV_ITEM_ID")]
    [InverseProperty("InventoryMasters")]
    public virtual ItemMaster INV_ITEM { get; set; }

    [ForeignKey("INV_ITEM_LOCATION_ID")]
    [InverseProperty("InventoryMasters")]
    public virtual ItemLocationMaster INV_ITEM_LOCATION { get; set; }
}
