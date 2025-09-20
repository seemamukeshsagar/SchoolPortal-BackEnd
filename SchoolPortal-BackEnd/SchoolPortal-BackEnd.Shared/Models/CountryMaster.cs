using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("CountryMaster")]
public partial class CountryMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CountryName { get; set; }

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

    [InverseProperty("Country")]
    public virtual ICollection<CompanyMaster> CompanyMasters { get; set; } = new List<CompanyMaster>();

    [InverseProperty("BankCountry")]
    public virtual ICollection<SchoolMaster> SchoolMasterBankCountries { get; set; } = new List<SchoolMaster>();

    [InverseProperty("Country")]
    public virtual ICollection<SchoolMaster> SchoolMasterCountries { get; set; } = new List<SchoolMaster>();

    [InverseProperty("JudistrictionCountry")]
    public virtual ICollection<SchoolMaster> SchoolMasterJudistrictionCountries { get; set; } = new List<SchoolMaster>();

    [InverseProperty("Country")]
    public virtual ICollection<StateMaster> StateMasters { get; set; } = new List<StateMaster>();
}
