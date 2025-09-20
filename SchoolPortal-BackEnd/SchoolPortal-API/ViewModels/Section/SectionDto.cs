using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.Section
{
    public class SectionDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
