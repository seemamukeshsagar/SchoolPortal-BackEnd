using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.Teacher
{
    public class TeacherDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters")]
        public string FirstName { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime Dob { get; set; }

        public DateTime? Doj { get; set; }
        public DateTime? DateOfLeaving { get; set; }

        [StringLength(500, ErrorMessage = "Address cannot be longer than 500 characters")]
        public string? Address { get; set; }

        public Guid? CityId { get; set; }
        public Guid? StateId { get; set; }
        
        [StringLength(20, ErrorMessage = "Zip code cannot be longer than 20 characters")]
        public string? ZipCode { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = null!;

        public string? MaritalStatus { get; set; }
        public string? Image { get; set; }
        
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters")]
        public string? Phone { get; set; }

        [Phone(ErrorMessage = "Invalid mobile number")]
        [StringLength(20, ErrorMessage = "Mobile number cannot be longer than 20 characters")]
        public string? MobilePhone { get; set; }

        [StringLength(50, ErrorMessage = "Years of experience cannot be longer than 50 characters")]
        public string? YearsOfExperience { get; set; }

        [StringLength(200, ErrorMessage = "Previous school cannot be longer than 200 characters")]
        public string? PreviousSchool { get; set; }

        [StringLength(20, ErrorMessage = "Salutation cannot be longer than 20 characters")]
        public string? Salutation { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        public Guid SchoolId { get; set; }

        public bool IsActive { get; set; } = true;
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
    }
}
