using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class LocationMaster
{
    public Guid Id { get; set; }

    public string? LocationMasterCode { get; set; }

    public string? LocationMasterName { get; set; }

    public Guid LocationMasterCity { get; set; }

    public bool LocationMasterIsActive { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }
}
