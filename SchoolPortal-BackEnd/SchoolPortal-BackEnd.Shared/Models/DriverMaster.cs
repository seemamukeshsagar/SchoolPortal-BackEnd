using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("DriverMaster")]
public partial class DriverMaster
{
    [Key]
    public int DRIVER_ID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_FIRST_NAME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_LAST_NAME { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DRIVER_DOB { get; set; }

    public int? DRIVER_QUAL_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_MOBILE_PHONE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_PHONE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_CITY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_STATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_ZIPCODE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string DRIVER_IMAGE { get; set; }

    public int DRIVER_CMP_ID { get; set; }

    public int DRIVER_SCH_ID { get; set; }

    public bool DRIVER_IS_ACTIVE { get; set; }

    public int? DRIVER_CITY_ID { get; set; }

    public int? DRIVER_STATE_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_LICENCE_NUMBER { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DRIVER_LICENCE_ISSUE_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DRIVER_LICENCE_VALID_DATE { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string DRIVER_LICENCE_DESCRIPTION { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string DRIVER_LICENCE_IMAGE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DRIVER_LICENCE_TYPE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string DRIVER_FATHER_NAME { get; set; }

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
