using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("LeaveTypeMaster")]
public partial class LeaveTypeMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string LeaveTypeCode { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string LeaveTypeDescription { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ApplicableGender { get; set; }

    public bool IsSpecialLeave { get; set; }

    public bool IsEncashable { get; set; }

    public bool IsCarryForward { get; set; }

    public bool LeaveTypeIsActive { get; set; }

    public Guid LeaveTypeCompanyId { get; set; }

    public Guid LeaveTypeSchoolId { get; set; }

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
