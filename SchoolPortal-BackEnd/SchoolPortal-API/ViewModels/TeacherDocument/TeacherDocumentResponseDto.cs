using System;

namespace SchoolPortal_API.ViewModels.TeacherDocument
{
    public class TeacherDocumentResponseDto
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string FileName { get; set; } = null!;
        public string? FileUrl { get; set; }
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
        public string? FileType => !string.IsNullOrEmpty(FileName) ? 
            Path.GetExtension(FileName).TrimStart('.').ToLower() : null;
    }
}
