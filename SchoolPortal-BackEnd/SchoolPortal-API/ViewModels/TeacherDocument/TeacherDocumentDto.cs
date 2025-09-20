using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.TeacherDocument
{
    public class TeacherDocumentDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Teacher ID is required")]
        public Guid TeacherId { get; set; }

        [Required(ErrorMessage = "Document name is required")]
        [StringLength(200, ErrorMessage = "Document name cannot be longer than 200 characters")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "File name is required")]
        [StringLength(500, ErrorMessage = "File name cannot be longer than 500 characters")]
        public string FileName { get; set; } = null!;

        [Required(ErrorMessage = "Company ID is required")]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "School ID is required")]
        public Guid SchoolId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
