using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class RouteDetail
{
    public Guid RouteDetailId { get; set; }

    public Guid RouteDetailRouteId { get; set; }

    public Guid RouteDetailVehicleId { get; set; }

    public Guid RouteDetailDriverId { get; set; }

    public Guid RouteDetailCleanerId { get; set; }

    public Guid RouteDetailsCompanyMasterId { get; set; }

    public Guid RouteDetailsSchoolMasterId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }
}
