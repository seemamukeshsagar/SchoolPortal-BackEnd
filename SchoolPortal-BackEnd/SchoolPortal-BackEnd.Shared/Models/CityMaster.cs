using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Shared.Models;

public partial class CityMaster
{
    public Guid Id { get; set; }

    public string? CityName { get; set; }

    public Guid CityStateId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }

    [InverseProperty("JudistrictionArea")]
    public virtual ICollection<CompanyMaster> CompanyMasterJudistrictionAreaNavigations { get; set; } = new List<CompanyMaster>();

    [InverseProperty("City")]
    public virtual ICollection<CompanyMaster> CompanyMasterCities { get; set; } = new List<CompanyMaster>();
    
    [InverseProperty("CityState")]
    public virtual StateMaster CityStateNavigation { get; set; } = null!;

    // Navigation properties for SchoolMaster relationships
    [InverseProperty("CityNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterCities { get; set; } = new List<SchoolMaster>();
    
    [InverseProperty("JudistrictionCityNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterJudistrictionCities { get; set; } = new List<SchoolMaster>();
    
    [InverseProperty("BankCityNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterBankCities { get; set; } = new List<SchoolMaster>();
    
    [InverseProperty("City")]
    public virtual StateMaster CityState { get; set; } = null!;
}
