using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("StudentMaster")]
[Index("RegistrationNumber", Name = "UQ_StudentMaster_RegistrationNumber", IsUnique = true)]
[Index("RollNumber", Name = "UQ_StudentMaster_RollNumber", IsUnique = true)]
public partial class StudentMaster
{
    [Key]
    public Guid StudentGUID { get; set; }

    public Guid RollNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string LastName { get; set; }

    [StringLength(150)]
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

    [StringLength(50)]
    [Unicode(false)]
    public string EmergencyContactNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DOB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DOJ { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string RegistrationNumber { get; set; }

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string HouseAllotted { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Transport { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Image { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; }

    public Guid CategoryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SiblingsIfAny { get; set; }

    public int? SiblingClassId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Gender { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DisabilityAny { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MedicalAlleryAny { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string BirthPlace { get; set; }

    public Guid BithCityId { get; set; }

    public Guid BirthStateId { get; set; }

    public Guid BithCountryId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string PreviousSchoolAttended { get; set; }

    public Guid PreviousSchoolClassId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PreviousSchoolPercentage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PreviousSchoolRank { get; set; }

    public Guid PreviousSchoolBoardId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PreviousSchoolFronDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PreviousSchoolToDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? WithdrawnDate { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string WithdrawnReason { get; set; }

    public Guid BloodGroupId { get; set; }

    public Guid Nationality { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Hobbies { get; set; }

    public Guid ReligionId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    public Guid RouteId { get; set; }

    public Guid RouteStopDetailsId { get; set; }

    public Guid ClassTeacherId { get; set; }

    public Guid RoutePickAndDrop { get; set; }

    public Guid FeesDiscountCategoryMasterId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TutionFees { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AnnialFees { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TransportFees { get; set; }

    public bool? UseTransportFees { get; set; }

    public Guid SessionId { get; set; }

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
