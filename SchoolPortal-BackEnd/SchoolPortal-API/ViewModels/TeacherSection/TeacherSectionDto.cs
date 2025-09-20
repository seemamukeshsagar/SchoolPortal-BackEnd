using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.TeacherSection
{
    public class TeacherSectionDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Teacher ID is required")]
        public Guid TeacherId { get; set; }

        [Required(ErrorMessage = "Class ID is required")]
        public Guid ClassId { get; set; }

        [Required(ErrorMessage = "Section ID is required")]
        public Guid SectionId { get; set; }

        [Required(ErrorMessage = "Subject ID is required")]
        public Guid SubjectId { get; set; }

        public bool IsClassTeacher { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        public Guid SchoolId { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public Guid CompanyId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
