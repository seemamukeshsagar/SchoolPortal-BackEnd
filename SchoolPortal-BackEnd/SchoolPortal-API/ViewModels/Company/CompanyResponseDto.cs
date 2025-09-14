using System;

namespace SchoolPortal_API.ViewModels.Company
{
    public class CompanyResponseDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Address { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; } = null!;
        public Guid StateId { get; set; }
        public string StateName { get; set; } = null!;
        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string? ZipCode { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public string? EstablishmentYear { get; set; }
        public Guid JurisdictionAreaId { get; set; }
        public string JurisdictionAreaName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
    }
}
