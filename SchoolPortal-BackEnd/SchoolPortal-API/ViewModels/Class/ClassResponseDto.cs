using System;

namespace SchoolPortal_API.ViewModels.Class
{
    public class ClassResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ExamAssessment { get; set; }
        public int? OrderBy { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SchoolId { get; set; }
        public bool IsActive { get; set; }
    }
}
