using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class RouteMaster
{
    public Guid RouteId { get; set; }

    public string? RouteCode { get; set; }

    public string? RouteName { get; set; }

    public string? RouteSession { get; set; }

    public Guid RouteStartLocationId { get; set; }

    public Guid RouteEndLocationId { get; set; }

    public Guid RouteApplicableClasses { get; set; }

    public Guid RouteCompanyMasterId { get; set; }

    public Guid RouteSchoolMasterId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }
}
