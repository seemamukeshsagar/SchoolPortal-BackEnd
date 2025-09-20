using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("ParentMaster")]
public partial class ParentMaster
{
    [Key]
    public Guid Id { get; set; }

    public Guid StudentGUID { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ParentFirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string ParentLastName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ParentDOB { get; set; }

    public int? QualifcationId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Occupation { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AnnualIncome { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Designation { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Mobile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Address { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string OfficeAddress { get; set; }

    public Guid OfficeCityId { get; set; }

    public Guid OfficeStateId { get; set; }

    public Guid OfficeCountryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OfficeZipCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OfficePhone { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Image { get; set; }

    public Guid RelationTypeId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid CompanyId { get; set; }

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
