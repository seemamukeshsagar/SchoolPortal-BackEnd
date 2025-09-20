using System;

namespace SchoolPortal_API.ViewModels.Teacher
{
    public class TeacherResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public DateTime Dob { get; set; }
        public DateTime? Doj { get; set; }
        public DateTime? DateOfLeaving { get; set; }
        public string? Address { get; set; }
        public Guid? CityId { get; set; }
        public string? CityName { get; set; }
        public Guid? StateId { get; set; }
        public string? StateName { get; set; }
        public string? ZipCode { get; set; }
        public string Gender { get; set; } = null!;
        public string? MaritalStatus { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public string? YearsOfExperience { get; set; }
        public string? PreviousSchool { get; set; }
        public string? Salutation { get; set; }
        public string? Email { get; set; }
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public Guid SchoolId { get; set; }
        public string? SchoolName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
        public int? Age => CalculateAge();

        private int? CalculateAge()
        {
            if (Dob == DateTime.MinValue) return null;
            
            var today = DateTime.Today;
            var age = today.Year - Dob.Year;
            
            // Adjust if the birthday hasn't occurred this year yet
            if (Dob.Date > today.AddYears(-age)) age--;
            
            return age;
        }
    }
}
