using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.Company
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(200, ErrorMessage = "Company name cannot be longer than 200 characters")]
        public string CompanyName { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "Address cannot be longer than 500 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public Guid CityId { get; set; }

        [Required(ErrorMessage = "State is required")]
        public Guid StateId { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public Guid CountryId { get; set; }

        [StringLength(20, ErrorMessage = "Zip code cannot be longer than 20 characters")]
        public string? ZipCode { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(4, MinimumLength = 4, ErrorMessage = "Establishment year must be 4 digits")]
        public string? EstablishmentYear { get; set; }

        [Required(ErrorMessage = "Jurisdiction area is required")]
        public Guid JurisdictionAreaId { get; set; }
    }
}
