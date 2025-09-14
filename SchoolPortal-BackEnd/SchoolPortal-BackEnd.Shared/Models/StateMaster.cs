using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Shared.Models;

public partial class StateMaster
{
    public Guid Id { get; set; }

    public string? StateName { get; set; }

    public Guid CountryId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }

    [InverseProperty("State")]
    public virtual ICollection<CompanyMaster> CompanyMasters { get; set; } = new List<CompanyMaster>();

    [InverseProperty("State")]
    public virtual ICollection<CityMaster> CityMasters { get; set; } = new List<CityMaster>();

    [InverseProperty("StateMasters")]
    public virtual CountryMaster Country { get; set; } = null!;

    // Navigation properties for SchoolMaster relationships
    [InverseProperty("StateNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterStates { get; set; } = new List<SchoolMaster>();
    
    [InverseProperty("JudistrictionStateNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterJudistrictionStates { get; set; } = new List<SchoolMaster>();
    
    [InverseProperty("BankStateNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterBankStates { get; set; } = new List<SchoolMaster>();
}
