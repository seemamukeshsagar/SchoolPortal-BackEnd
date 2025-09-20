using System;

namespace SchoolPortal_API.ViewModels.ClassSection
{
    public class ClassSectionResponseDto
    {
        public Guid Id { get; set; }
        public Guid ClassMasterId { get; set; }
        public string? ClassName { get; set; }
        public Guid SectionMasterId { get; set; }
        public string? SectionName { get; set; }
        public Guid LocationId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; }
    }
}
