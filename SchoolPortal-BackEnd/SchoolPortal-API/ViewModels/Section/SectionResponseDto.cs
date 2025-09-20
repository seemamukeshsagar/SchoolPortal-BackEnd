using System;

namespace SchoolPortal_API.ViewModels.Section
{
    public class SectionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; }
    }
}
