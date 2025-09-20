using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("SchoolContactMaster")]
public partial class SchoolContactMaster
{
    [Key]
    public Guid Id { get; set; }

    public Guid SchoolId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MobilePhone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AddressLine1 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AddressLine2 { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

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

    [StringLength(255)]
    public string StatusMessage { get; set; }
}
