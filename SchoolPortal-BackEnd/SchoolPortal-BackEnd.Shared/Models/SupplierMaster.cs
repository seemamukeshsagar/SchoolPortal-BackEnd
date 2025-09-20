using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SupplierMaster")]
public partial class SupplierMaster
{
    [Key]
    public int SUPPLIER_ID { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string SUPPLIER_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string SUPPLIER_DESCRIPTION { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string SUPPLIER_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SUPPLIER_CITY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SUPPLIER_STATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SUPPLIER_COUNTRY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SUPPLIER_ZIPCODE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SUPPLIER_PHONE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SUPPLIER_MOBILE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SUPPLIER_EMAIL { get; set; }

    public int? SUPPLIER_CITY_ID { get; set; }

    public int? SUPPLIER_STATE_ID { get; set; }

    public bool SUPPLIER_IS_ACTIVE { get; set; }

    public int SUPPLIER_CMP_ID { get; set; }

    public int SUPPLIER_SCH_ID { get; set; }

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
