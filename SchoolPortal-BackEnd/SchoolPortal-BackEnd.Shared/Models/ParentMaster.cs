using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class ParentMaster
{
    public Guid Id { get; set; }

    public Guid StudentGuid { get; set; }

    public string ParentFirstName { get; set; } = null!;

    public string ParentLastName { get; set; } = null!;

    public DateTime? ParentDob { get; set; }

    public int? QualifcationId { get; set; }

    public string? Occupation { get; set; }

    public decimal? AnnualIncome { get; set; }

    public string? Designation { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public Guid CityId { get; set; }

    public Guid StateId { get; set; }

    public Guid CountryId { get; set; }

    public string? ZipCode { get; set; }

    public string? OfficeAddress { get; set; }

    public Guid OfficeCityId { get; set; }

    public Guid OfficeStateId { get; set; }

    public Guid OfficeCountryId { get; set; }

    public string? OfficeZipCode { get; set; }

    public string? OfficePhone { get; set; }

    public string? Image { get; set; }

    public Guid RelationTypeId { get; set; }

    public Guid SchoolId { get; set; }

    public Guid CompanyId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? StatusMessage { get; set; }
}
