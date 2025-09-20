using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("AuthorMaster")]
public partial class AuthorMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Description { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Address { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Mobile { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }
}
