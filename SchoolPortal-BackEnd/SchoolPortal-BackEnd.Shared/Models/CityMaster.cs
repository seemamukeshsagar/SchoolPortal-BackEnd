using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("CityMaster")]
public partial class CityMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CityName { get; set; }

    public Guid CityStateId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string StatusMessage { get; set; }

    [ForeignKey("CityStateId")]
    [InverseProperty("CityMasters")]
    public virtual StateMaster CityState { get; set; }

    // Added to align with repository usage (CityStateNavigation)
    [ForeignKey("CityStateId")]
    public virtual StateMaster CityStateNavigation { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<CompanyMaster> CompanyMasterCities { get; set; } = new List<CompanyMaster>();

    [InverseProperty("JudistrictionAreaNavigation")]
    public virtual ICollection<CompanyMaster> CompanyMasterJudistrictionAreaNavigations { get; set; } = new List<CompanyMaster>();
}
