using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("LeaveStatusMaster")]
public partial class LeaveStatusMaster
{
    [Key]
    public int LEAVE_STATUS_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LEAVE_STATUS_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string LEAVE_STATUS_DESCRIPTION { get; set; }

    public bool LEAVE_STATUS_IS_ACTIVE { get; set; }

    public int LEAVE_STATUS_CMP_ID { get; set; }

    public int LEAVE_STATUS_SCH_ID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CREATED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CREATED_DATE { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string MODIFIED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime MODIFIED_DATE { get; set; }

    [InverseProperty("EMPLAD_STATUS")]
    public virtual ICollection<EmpLeaveAvailDetail> EmpLeaveAvailDetails { get; set; } = new List<EmpLeaveAvailDetail>();
}
