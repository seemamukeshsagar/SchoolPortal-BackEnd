using System;

namespace SchoolPortal_API.ViewModels.TeacherClass
{
    public class TeacherClassResponseDto
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public Guid ClassId { get; set; }
        public string? ClassName { get; set; }
        public Guid SectionId { get; set; }
        public string? SectionName { get; set; }
        public Guid SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public Guid CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public Guid SchoolId { get; set; }
        public string? SchoolName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
    }
}
