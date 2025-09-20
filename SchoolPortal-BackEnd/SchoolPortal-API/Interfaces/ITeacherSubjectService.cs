using SchoolPortal_API.ViewModels.TeacherSubject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherSubjectService
    {
        Task<IEnumerable<TeacherSubjectResponseDto>> GetAllTeacherSubjectsAsync();
        Task<TeacherSubjectResponseDto?> GetTeacherSubjectByIdAsync(Guid id);
        Task<IEnumerable<TeacherSubjectResponseDto>> GetSubjectsByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherSubjectResponseDto>> GetTeachersBySubjectIdAsync(Guid subjectId);
        Task<IEnumerable<TeacherSubjectResponseDto>> GetByTeacherAndClassAsync(Guid teacherId, Guid classId);
        Task<TeacherSubjectResponseDto?> AddTeacherSubjectAsync(TeacherSubjectDto teacherSubjectDto, Guid userId);
        Task<TeacherSubjectResponseDto?> UpdateTeacherSubjectAsync(Guid id, TeacherSubjectDto teacherSubjectDto, Guid userId);
        Task<bool> RemoveTeacherSubjectAsync(Guid id, Guid userId);
        Task<bool> IsTeacherAssignedToSubjectAsync(Guid teacherId, Guid subjectId, Guid classId);
    }
}
