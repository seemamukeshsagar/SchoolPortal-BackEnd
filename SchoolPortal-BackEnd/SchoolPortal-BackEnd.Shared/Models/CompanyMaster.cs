using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("CompanyMaster")]
public partial class CompanyMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CompanyName { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Description { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Address { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EstablishmentYear { get; set; }

    public Guid JudistrictionArea { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string StatusMessage { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("CompanyMasterCities")]
    public virtual CityMaster City { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("CompanyMasters")]
    public virtual CountryMaster Country { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<DeptDesigDetail> DeptDesigDetails { get; set; } = new List<DeptDesigDetail>();

    [InverseProperty("Company")]
    public virtual ICollection<DeptMaster> DeptMasters { get; set; } = new List<DeptMaster>();

    [InverseProperty("Company")]
    public virtual ICollection<DesigGradeDetail> DesigGradeDetails { get; set; } = new List<DesigGradeDetail>();

    [InverseProperty("Company")]
    public virtual ICollection<DesigMaster> DesigMasters { get; set; } = new List<DesigMaster>();

    [ForeignKey("JudistrictionArea")]
    [InverseProperty("CompanyMasterJudistrictionAreaNavigations")]
    public virtual CityMaster JudistrictionAreaNavigation { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<RoleMaster> RoleMasters { get; set; } = new List<RoleMaster>();

    [InverseProperty("Company")]
    public virtual ICollection<SchoolMaster> SchoolMasters { get; set; } = new List<SchoolMaster>();

    [ForeignKey("StateId")]
    [InverseProperty("CompanyMasters")]
    public virtual StateMaster State { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
