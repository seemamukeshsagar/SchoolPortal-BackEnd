using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ItemTypeMaster")]
public partial class ItemTypeMaster
{
    [Key]
    public int ITEM_TYPE_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ITEM_TYPE_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string ITEM_TYPE_DESCRIPTION { get; set; }

    public bool? ITEM_TYPE_IS_ACTIVE { get; set; }

    public int ITEM_TYPE_CMP_ID { get; set; }

    public int ITEM_TYPE_SCH_ID { get; set; }

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

    [InverseProperty("ITEM_TYPE_MASTER")]
    public virtual ICollection<ItemMaster> ItemMasters { get; set; } = new List<ItemMaster>();
}
