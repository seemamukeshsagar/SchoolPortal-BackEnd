using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("VendorMaster")]
public partial class VendorMaster
{
    [Key]
    public int VENDOR_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VENDOR_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string VENDOR_DESC { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string VENDOR_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VENDOR_CITY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VENDOR_STATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VENDOR_ZIPCODE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VENDOR_PHONE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VENDOR_MOBILE_PHONE { get; set; }

    public bool VENDOR_IS_ACTIVE { get; set; }

    public int VENDOR_CMP_ID { get; set; }

    public int VENDOR_SCH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VENDOR_EMAIL { get; set; }

    public int? VENDOR_CITY_ID { get; set; }

    public int? VENDOR_STATE_ID { get; set; }

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

    [InverseProperty("BILL_VENDOR")]
    public virtual ICollection<BillMaster> BillMasters { get; set; } = new List<BillMaster>();
}
