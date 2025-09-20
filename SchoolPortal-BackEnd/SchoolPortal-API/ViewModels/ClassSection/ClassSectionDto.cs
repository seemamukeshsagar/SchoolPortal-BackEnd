using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.ClassSection
{
    public class ClassSectionDto
    {
        [Required]
        public Guid ClassMasterId { get; set; }
        [Required]
        public Guid SectionMasterId { get; set; }
        public Guid LocationId { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
