using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class EmpLeaveAvailDetail
{
    [Key]
    public int EMPLAD_ID { get; set; }

    public int EMPLAD_EMP_ID { get; set; }

    public int EMPLAD_LEAVE_TYPE_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EMPLAD_APPLY_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EMPLAD_START_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EMPLAD_END_DATE { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal? EMPLAD_TOTAL_DAYS { get; set; }

    public bool EMPLAD_IS_HALF_DAY { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string EMPLAD_REASON { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EMPLAD_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMPLAD_CONTACT_NUMBER { get; set; }

    public int EMPLAD_STATUS_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMPLAD_SESSION { get; set; }

    public int EMPLAD_CMP_ID { get; set; }

    public int EMPLAD_SCH_ID { get; set; }

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

    [ForeignKey("EMPLAD_EMP_ID")]
    [InverseProperty("EmpLeaveAvailDetails")]
    public virtual EmpMaster EMPLAD_EMP { get; set; }

    [ForeignKey("EMPLAD_STATUS_ID")]
    [InverseProperty("EmpLeaveAvailDetails")]
    public virtual LeaveStatusMaster EMPLAD_STATUS { get; set; }
}
