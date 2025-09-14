using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class RouteStopDetail
{
    public Guid RouteStopId { get; set; }

    public Guid RouteStopRouteDetailId { get; set; }

    public Guid RouteStopRouteId { get; set; }

    public Guid RouteStopLocationId { get; set; }

    public int RouteStopNumber { get; set; }

    public string RouteStopPickupTime { get; set; } = null!;

    public string RouteStopDropTime { get; set; } = null!;

    public decimal? RouteStopOneWayMonthlyFee { get; set; }

    public decimal? RouteStopTwoWayMonthlyFee { get; set; }

    public Guid RouteStopCompanyMasterId { get; set; }

    public Guid RouteStopSchoolMasterId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusMessage { get; set; }
}
