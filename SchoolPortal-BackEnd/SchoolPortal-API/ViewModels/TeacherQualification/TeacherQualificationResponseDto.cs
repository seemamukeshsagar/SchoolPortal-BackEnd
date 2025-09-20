using System;

namespace SchoolPortal_API.ViewModels.TeacherQualification
{
    public class TeacherQualificationResponseDto
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public Guid QualificationId { get; set; }
        public string? QualificationName { get; set; }
        public string? QualificationCode { get; set; }
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
