using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class RouteStopDetail
{
    [Key]
    public Guid RouteStopId { get; set; }

    public Guid RouteStopRouteDetailId { get; set; }

    public Guid RouteStopRouteId { get; set; }

    public Guid RouteStopLocationId { get; set; }

    public int RouteStopNumber { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string RouteStopPickupTime { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string RouteStopDropTime { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RouteStopOneWayMonthlyFee { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RouteStopTwoWayMonthlyFee { get; set; }

    public Guid RouteStopCompanyMasterId { get; set; }

    public Guid RouteStopSchoolMasterId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

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
}
