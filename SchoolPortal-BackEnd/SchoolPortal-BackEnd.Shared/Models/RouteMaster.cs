using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("RouteMaster")]
public partial class RouteMaster
{
    [Key]
    public Guid RouteId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RouteCode { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string RouteName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RouteSession { get; set; }

    public Guid RouteStartLocationId { get; set; }

    public Guid RouteEndLocationId { get; set; }

    public Guid RouteApplicableClasses { get; set; }

    public Guid RouteCompanyMasterId { get; set; }

    public Guid RouteSchoolMasterId { get; set; }

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
