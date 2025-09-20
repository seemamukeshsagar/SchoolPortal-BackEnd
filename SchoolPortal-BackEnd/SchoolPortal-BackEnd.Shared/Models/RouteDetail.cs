using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class RouteDetail
{
    [Key]
    public Guid RouteDetailId { get; set; }

    public Guid RouteDetail_RouteId { get; set; }

    public Guid RouteDetailVehicleId { get; set; }

    public Guid RouteDetail_DriverId { get; set; }

    public Guid RouteDetailCleanerId { get; set; }

    public Guid RouteDetailsCompanyMasterId { get; set; }

    public Guid RouteDetailsSchoolMasterId { get; set; }

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
