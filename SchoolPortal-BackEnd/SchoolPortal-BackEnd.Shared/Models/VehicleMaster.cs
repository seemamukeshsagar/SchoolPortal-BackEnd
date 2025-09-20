using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("VehicleMaster")]
public partial class VehicleMaster
{
    [Key]
    public int VEH_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VEH_NUMBER { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VEH_MODEL { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VEH_MAKE { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string VEH_TYPE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VEH_REGISTRATION { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VEH_INSURANCE_COMPANY { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? VEH_INSURANCE_PREMIUM { get; set; }

    public int? VEH_SEAT_CAPACITY { get; set; }

    public bool VEH_IS_ACTIVE { get; set; }

    public int VEH_CMP_ID { get; set; }

    public int VEH_SCH_ID { get; set; }

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

    [InverseProperty("BILL_VEHICLE")]
    public virtual ICollection<BillMaster> BillMasters { get; set; } = new List<BillMaster>();

    [InverseProperty("VEHEXP_VEHICEL")]
    public virtual ICollection<VehicleExpenseDetail> VehicleExpenseDetails { get; set; } = new List<VehicleExpenseDetail>();
}
