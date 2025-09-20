using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("RegistrationMaster")]
public partial class RegistrationMaster
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string RegistrationNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DOB { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Age { get; set; }

    public Guid ClassMasterId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    public Guid SessionId { get; set; }

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
    public string ContactNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    public Guid ReligionId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid Gender { get; set; }

    public Guid BloodGroupId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RegistrationFees { get; set; }

    public bool? RegistrationFeesPaid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SiblingsIfAny { get; set; }

    public Guid SiblingClassMasterId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FathersFirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FathersLastName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FathersDOB { get; set; }

    public Guid FathersQualificationId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FathersOccupation { get; set; }

    public Guid FathersDesignationId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? FathersAnnualIncome { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MothersFirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MothersLastName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MothersDOB { get; set; }

    public Guid MothersQualificationId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MothersOccupation { get; set; }

    public Guid MothersDesignationId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MothersAnnualIncome { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string BirthCertificate { get; set; }

    public bool? BirthCertificateSubmitted { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string TransferCertificate { get; set; }

    public bool? TransferCertificateSubmitted { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ReportCardLastClass { get; set; }

    public bool? ReportCardSubmitted { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string OtherCertificateName { get; set; }

    public bool? OtherCertificateSubmitted { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string OtherDescription { get; set; }

    public bool? IsAdmissionConfirmed { get; set; }

    public Guid? StudentGuid { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

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
