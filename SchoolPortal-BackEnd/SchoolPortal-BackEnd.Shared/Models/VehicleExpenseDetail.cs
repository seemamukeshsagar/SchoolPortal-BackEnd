using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class VehicleExpenseDetail
{
    [Key]
    public int VEHEXP_ID { get; set; }

    public int VEHEXP_VEHICEL_ID { get; set; }

    public int VEHEXP_TYPE_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VEHEXP_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string VEHEXP_DESC { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? VEHEXP_DATE { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? VEHEXP_AMOUNT { get; set; }

    public int VEHEXP_CMP_ID { get; set; }

    public int VEHEXP_SCH_ID { get; set; }

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

    [ForeignKey("VEHEXP_VEHICEL_ID")]
    [InverseProperty("VehicleExpenseDetails")]
    public virtual VehicleMaster VEHEXP_VEHICEL { get; set; }
}
