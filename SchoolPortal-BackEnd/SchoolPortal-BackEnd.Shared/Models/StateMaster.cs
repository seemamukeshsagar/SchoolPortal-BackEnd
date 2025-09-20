using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StateMaster")]
public partial class StateMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string StateName { get; set; }

    public Guid CountryId { get; set; }

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

    [StringLength(250)]
    [Unicode(false)]
    public string StatusMessage { get; set; }

    [InverseProperty("CityState")]
    public virtual ICollection<CityMaster> CityMasters { get; set; } = new List<CityMaster>();

    [InverseProperty("State")]
    public virtual ICollection<CompanyMaster> CompanyMasters { get; set; } = new List<CompanyMaster>();

    [ForeignKey("CountryId")]
    [InverseProperty("StateMasters")]
    public virtual CountryMaster Country { get; set; }
}
