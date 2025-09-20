using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.TeacherSubject
{
    public class TeacherSubjectDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Teacher ID is required")]
        public Guid TeacherId { get; set; }

        [Required(ErrorMessage = "Subject ID is required")]
        public Guid SubjectId { get; set; }

        [Required(ErrorMessage = "Class ID is required")]
        public Guid ClassMasterId { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        public Guid SchoolId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
