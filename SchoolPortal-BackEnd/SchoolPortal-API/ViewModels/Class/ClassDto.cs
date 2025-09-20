using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.Class
{
    public class ClassDto
    {
        [Required]
        public string Name { get; set; }
        public string? ExamAssessment { get; set; }
        public int? OrderBy { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
