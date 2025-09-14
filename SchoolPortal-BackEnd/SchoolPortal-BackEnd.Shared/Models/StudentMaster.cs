using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class StudentMaster
{
    public Guid StudentGuid { get; set; }

    public Guid RollNumber { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    public string? ZipCode { get; set; }

    public string? ContactNumber { get; set; }

    public string? EmergencyContactNumber { get; set; }

    public DateTime Dob { get; set; }

    public DateTime Doj { get; set; }

    public string RegistrationNumber { get; set; } = null!;

    public Guid ClassId { get; set; }

    public Guid SectionId { get; set; }

    public string? HouseAllotted { get; set; }

    public string? Transport { get; set; }

    public string? Image { get; set; }

    public string? Email { get; set; }

    public Guid CategoryId { get; set; }

    public string? SiblingsIfAny { get; set; }

    public int? SiblingClassId { get; set; }

    public string? Gender { get; set; }

    public string? DisabilityAny { get; set; }

    public string? MedicalAlleryAny { get; set; }

    public string? BirthPlace { get; set; }

    public Guid BithCityId { get; set; }

    public Guid BirthStateId { get; set; }

    public Guid BithCountryId { get; set; }

    public string? PreviousSchoolAttended { get; set; }

    public Guid PreviousSchoolClassId { get; set; }

    public decimal? PreviousSchoolPercentage { get; set; }

    public string? PreviousSchoolRank { get; set; }

    public Guid PreviousSchoolBoardId { get; set; }

    public DateTime? PreviousSchoolFronDate { get; set; }

    public DateTime? PreviousSchoolToDate { get; set; }

    public DateTime? WithdrawnDate { get; set; }

    public string? WithdrawnReason { get; set; }

    public Guid BloodGroupId { get; set; }

    public Guid Nationality { get; set; }

    public string? Hobbies { get; set; }

    public Guid ReligionId { get; set; }

    public string? Phone { get; set; }

    public Guid RouteId { get; set; }

    public Guid RouteStopDetailsId { get; set; }

    public Guid ClassTeacherId { get; set; }

    public Guid RoutePickAndDrop { get; set; }

    public Guid FeesDiscountCategoryMasterId { get; set; }

    public decimal? TutionFees { get; set; }

    public decimal? AnnialFees { get; set; }

    public decimal? TransportFees { get; set; }

    public bool? UseTransportFees { get; set; }

    public Guid SessionId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }
}
