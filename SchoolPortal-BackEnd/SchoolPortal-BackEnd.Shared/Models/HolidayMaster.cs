using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("HolidayMaster")]
public partial class HolidayMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string HolidayName { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string HolidayDescription { get; set; }

    public Guid HolidayTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime HolidayFromDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime HolidayToDate { get; set; }

    public Guid HolidayYear { get; set; }

    public bool? HolidayIsActive { get; set; }

    public Guid HolidayCompanyId { get; set; }

    public Guid HolidaySchoolId { get; set; }

    public bool? HolidayIsStaffApplicable { get; set; }

    public Guid SessionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }
}
