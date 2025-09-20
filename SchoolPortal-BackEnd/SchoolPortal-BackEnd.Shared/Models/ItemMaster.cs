using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ItemMaster")]
public partial class ItemMaster
{
    [Key]
    public int ITEM_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ITEM_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string ITEM_DESCRIPTION { get; set; }

    public int? ITEM_TYPE_MASTER_ID { get; set; }

    public bool? ITEM_IS_ACTIVE { get; set; }

    public int ITEM_CMP_ID { get; set; }

    public int ITEM_SCH_ID { get; set; }

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

    [ForeignKey("ITEM_TYPE_MASTER_ID")]
    [InverseProperty("ItemMasters")]
    public virtual ItemTypeMaster ITEM_TYPE_MASTER { get; set; }

    [InverseProperty("INV_ITEM")]
    public virtual ICollection<InventoryMaster> InventoryMasters { get; set; } = new List<InventoryMaster>();
}
