using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPortal.Shared.Models;

public partial class SchoolMaster
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Email { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public Guid? CityId { get; set; }

    public Guid? StateId { get; set; }

    public Guid? CountryId { get; set; }

    public string? ZipCode { get; set; }

    public string? Phone { get; set; }

    public string? EstablishmentYear { get; set; }

    public string? Mobile { get; set; }

    public Guid? JudistrictionCityId { get; set; }

    public Guid? JudistrictionStateId { get; set; }

    public Guid? JudistrictionCountryId { get; set; }

    public string? BankName { get; set; }

    public string? BankAddress1 { get; set; }

    public string? BankAddress2 { get; set; }

    public Guid? BankCityId { get; set; }

    public Guid? BankStateId { get; set; }

    public Guid? BankCountryId { get; set; }

    public string? BankZipCode { get; set; }

    public string? AccountNumber { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }

    public virtual CountryMaster? BankCountry { get; set; }

    public virtual CompanyMaster Company { get; set; } = null!;

    public virtual CountryMaster? Country { get; set; }

    public virtual StateMaster? State { get; set; }

    public virtual CityMaster? City { get; set; }

    public virtual StateMaster? JudistrictionState { get; set; }

    public virtual CityMaster? JudistrictionCity { get; set; }

    public virtual StateMaster? BankState { get; set; }

    public virtual CityMaster? BankCity { get; set; }

    // Collection navigation properties are defined below with InverseProperty attributes

    public virtual CountryMaster? JudistrictionCountry { get; set; }

    // Navigation properties for CityMaster relationships
    [ForeignKey("CityId")]
    public virtual CityMaster? CityNavigation { get; set; }

    [ForeignKey("JudistrictionCityId")]
    public virtual CityMaster? JudistrictionCityNavigation { get; set; }

    [ForeignKey("BankCityId")]
    public virtual CityMaster? BankCityNavigation { get; set; }
    
    // Navigation properties for StateMaster relationships
    [ForeignKey("StateId")]
    public virtual StateMaster? StateNavigation { get; set; }
    
    [ForeignKey("JudistrictionStateId")]
    public virtual StateMaster? JudistrictionStateNavigation { get; set; }
    
    [ForeignKey("BankStateId")]
    public virtual StateMaster? BankStateNavigation { get; set; }
    
    // Navigation properties for CountryMaster relationships
    [ForeignKey("CountryId")]
    public virtual CountryMaster? CountryNavigation { get; set; }
    
    [ForeignKey("JudistrictionCountryId")]
    public virtual CountryMaster? JudistrictionCountryNavigation { get; set; }
    
    [ForeignKey("BankCountryId")]
    public virtual CountryMaster? BankCountryNavigation { get; set; }

    [InverseProperty("School")]
    public virtual ICollection<RoleMaster> RoleMasters { get; set; } = new List<RoleMaster>();

    [InverseProperty("School")]
    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();

    [InverseProperty("School")]
    public virtual ICollection<DeptMaster> DeptMasters { get; set; } = new List<DeptMaster>();

    [InverseProperty("School")]
    public virtual ICollection<DesigMaster> DesigMasters { get; set; } = new List<DesigMaster>();

    [InverseProperty("School")]
    public virtual ICollection<DeptDesigDetail> DeptDesigDetails { get; set; } = new List<DeptDesigDetail>();

    [InverseProperty("School")]
    public virtual ICollection<DesigGradeDetail> DesigGradeDetails { get; set; } = new List<DesigGradeDetail>();
}
