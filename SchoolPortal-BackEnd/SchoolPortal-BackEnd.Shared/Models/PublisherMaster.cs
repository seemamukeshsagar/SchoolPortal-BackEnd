using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("PublisherMaster")]
public partial class PublisherMaster
{
    [Key]
    public int PUBLISHER_ID { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string PUBLISHER_NAME { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string PUBLISHER_DESCRIPTION { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string PUBLISHER_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PUBLISHER_CITY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PUBLISHER_STATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PUBLISHER_COUNTRY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PUBLISHER_ZIPCODE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PUBLISHER_PHONE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PUBLISHER_MOBILE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string PUBLISHER_EMAIL { get; set; }

    public int? PUBLISHER_CITY_ID { get; set; }

    public int? PUBLISHER_STATE_ID { get; set; }

    public bool PUBLISHER_IS_ACTIVE { get; set; }

    public int PUBLISHER_CMP_ID { get; set; }

    public int PUBLISHER_SCH_ID { get; set; }

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
