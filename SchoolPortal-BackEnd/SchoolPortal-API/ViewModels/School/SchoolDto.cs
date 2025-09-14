using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.School
{
    public class SchoolDto
    {
        [Required(ErrorMessage = "School name is required")]
        [StringLength(200, ErrorMessage = "School name cannot be longer than 200 characters")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string? Description { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        public string? Email { get; set; }

        [StringLength(500, ErrorMessage = "Address line 1 cannot be longer than 500 characters")]
        public string? Address1 { get; set; }

        [StringLength(500, ErrorMessage = "Address line 2 cannot be longer than 500 characters")]
        public string? Address2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public Guid? CityId { get; set; }

        [Required(ErrorMessage = "State is required")]
        public Guid? StateId { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public Guid? CountryId { get; set; }

        [StringLength(20, ErrorMessage = "Zip code cannot be longer than 20 characters")]
        public string? ZipCode { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }

        [StringLength(4, MinimumLength = 4, ErrorMessage = "Establishment year must be 4 digits")]
        public string? EstablishmentYear { get; set; }

        [Phone(ErrorMessage = "Invalid mobile number format")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "Jurisdiction city is required")]
        public Guid? JurisdictionCityId { get; set; }

        public Guid? JurisdictionStateId { get; set; }
        public Guid? JurisdictionCountryId { get; set; }

        [StringLength(100, ErrorMessage = "Bank name cannot be longer than 100 characters")]
        public string? BankName { get; set; }

        [StringLength(500, ErrorMessage = "Bank address line 1 cannot be longer than 500 characters")]
        public string? BankAddress1 { get; set; }

        [StringLength(500, ErrorMessage = "Bank address line 2 cannot be longer than 500 characters")]
        public string? BankAddress2 { get; set; }

        public Guid? BankCityId { get; set; }
        public Guid? BankStateId { get; set; }
        public Guid? BankCountryId { get; set; }

        [StringLength(20, ErrorMessage = "Bank zip code cannot be longer than 20 characters")]
        public string? BankZipCode { get; set; }

        [StringLength(50, ErrorMessage = "Account number cannot be longer than 50 characters")]
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public Guid CompanyId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
