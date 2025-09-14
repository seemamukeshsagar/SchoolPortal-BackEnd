using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Shared.Models;

public partial class CountryMaster
{
    public Guid Id { get; set; }

    public string? CountryName { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }

    public virtual ICollection<CompanyMaster> CompanyMasters { get; set; } = new List<CompanyMaster>();

    [InverseProperty("BankCountryNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterBankCountries { get; set; } = new List<SchoolMaster>();

    [InverseProperty("CountryNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterCountries { get; set; } = new List<SchoolMaster>();

    [InverseProperty("JudistrictionCountryNavigation")]
    public virtual ICollection<SchoolMaster> SchoolMasterJudistrictionCountries { get; set; } = new List<SchoolMaster>();

    [InverseProperty("Country")]
    public virtual ICollection<StateMaster> StateMasters { get; set; } = new List<StateMaster>();
}
