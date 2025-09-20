using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("GenderMaster")]
public partial class GenderMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Gender { get; set; }

    public bool IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("GenderMasterCreatedByNavigations")]
    public virtual UserDetail CreatedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("GenderMasterModifiedByNavigations")]
    public virtual UserDetail ModifiedByNavigation { get; set; }
}
