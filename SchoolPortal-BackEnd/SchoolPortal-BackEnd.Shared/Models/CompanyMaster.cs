using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class CompanyMaster
{
    public Guid Id { get; set; }

    public string? CompanyName { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    public string? ZipCode { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? EstablishmentYear { get; set; }

    public Guid JudistrictionArea { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }

    public virtual CityMaster City { get; set; } = null!;

    public virtual CountryMaster Country { get; set; } = null!;

    public virtual ICollection<DeptDesigDetail> DeptDesigDetails { get; set; } = new List<DeptDesigDetail>();

    public virtual ICollection<DeptMaster> DeptMasters { get; set; } = new List<DeptMaster>();

    public virtual ICollection<DesigGradeDetail> DesigGradeDetails { get; set; } = new List<DesigGradeDetail>();

    public virtual ICollection<DesigMaster> DesigMasters { get; set; } = new List<DesigMaster>();

    public virtual CityMaster JudistrictionAreaNavigation { get; set; } = null!;

    public virtual ICollection<RoleMaster> RoleMasters { get; set; } = new List<RoleMaster>();

    public virtual ICollection<SchoolMaster> SchoolMasters { get; set; } = new List<SchoolMaster>();

    public virtual StateMaster State { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
