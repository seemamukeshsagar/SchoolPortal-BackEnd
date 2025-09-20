using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.TeacherQualification
{
    public class TeacherQualificationDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Teacher ID is required")]
        public Guid TeacherId { get; set; }

        [Required(ErrorMessage = "Qualification ID is required")]
        public Guid QualificationId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
