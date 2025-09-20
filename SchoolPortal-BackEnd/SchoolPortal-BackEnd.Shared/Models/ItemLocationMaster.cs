using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ItemLocationMaster")]
public partial class ItemLocationMaster
{
    [Key]
    public int ITEM_LOCATION_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ITEM_LOCATION_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string ITEM_LOCATION_DESCRIPTION { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ITEM_LOCATION_BUILDING { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ITEM_LOCATION_FLOOR { get; set; }

    public int? ITEM_LOCATION_NUMBER { get; set; }

    public int? ITEM_LOCATION_CAPACITY { get; set; }

    public bool? ITEM_LOCATION_IS_ACTIVE { get; set; }

    public int ITEM_LOCATION_CMP_ID { get; set; }

    public int ITEM_LOCATION_SCH_ID { get; set; }

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

    [InverseProperty("INV_ITEM_LOCATION")]
    public virtual ICollection<InventoryMaster> InventoryMasters { get; set; } = new List<InventoryMaster>();
}
