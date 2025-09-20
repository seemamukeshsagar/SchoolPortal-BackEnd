using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SchoolMaster")]
public partial class SchoolMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Description { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Address1 { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Address2 { get; set; }

    public Guid? CityId { get; set; }

    public Guid? StateId { get; set; }

    public Guid? CountryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string EstablishmentYear { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Mobile { get; set; }

    public Guid? JudistrictionCityId { get; set; }

    public Guid? JudistrictionStateId { get; set; }

    public Guid? JudistrictionCountryId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string BankName { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string BankAddress1 { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string BankAddress2 { get; set; }

    public Guid? BankCityId { get; set; }

    public Guid? BankStateId { get; set; }

    public Guid? BankCountryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string BankZipCode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AccountNumber { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CompanyId { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string StatusMessage { get; set; }

    [ForeignKey("BankCountryId")]
    [InverseProperty("SchoolMasterBankCountries")]
    public virtual CountryMaster BankCountry { get; set; }

    // Newly added navigation properties to align with repository usage
    [ForeignKey("BankStateId")]
    public virtual StateMaster BankState { get; set; }

    [ForeignKey("BankCityId")]
    public virtual CityMaster BankCity { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("SchoolMasters")]
    public virtual CompanyMaster Company { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("SchoolMasterCountries")]
    public virtual CountryMaster Country { get; set; }

    [ForeignKey("StateId")]
    public virtual StateMaster State { get; set; }

    [ForeignKey("CityId")]
    public virtual CityMaster City { get; set; }

    [InverseProperty("School")]
    public virtual ICollection<DeptDesigDetail> DeptDesigDetails { get; set; } = new List<DeptDesigDetail>();

    [InverseProperty("School")]
    public virtual ICollection<DeptMaster> DeptMasters { get; set; } = new List<DeptMaster>();

    [InverseProperty("School")]
    public virtual ICollection<DesigGradeDetail> DesigGradeDetails { get; set; } = new List<DesigGradeDetail>();

    [InverseProperty("School")]
    public virtual ICollection<DesigMaster> DesigMasters { get; set; } = new List<DesigMaster>();

    [ForeignKey("JudistrictionCountryId")]
    [InverseProperty("SchoolMasterJudistrictionCountries")]
    public virtual CountryMaster JudistrictionCountry { get; set; }

    [ForeignKey("JudistrictionStateId")]
    public virtual StateMaster JudistrictionState { get; set; }

    [ForeignKey("JudistrictionCityId")]
    public virtual CityMaster JudistrictionCity { get; set; }

    [InverseProperty("School")]
    public virtual ICollection<RoleMaster> RoleMasters { get; set; } = new List<RoleMaster>();

    [InverseProperty("School")]
    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
