using SchoolPortal_API.ViewModels.TeacherClass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherClassService
    {
        Task<IEnumerable<TeacherClassResponseDto>> GetAllTeacherClassesAsync();
        Task<TeacherClassResponseDto?> GetTeacherClassByIdAsync(Guid id);
        Task<IEnumerable<TeacherClassResponseDto>> GetClassesByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherClassResponseDto>> GetTeachersByClassIdAsync(Guid classId);
        Task<IEnumerable<TeacherClassResponseDto>> GetByTeacherAndClassAsync(Guid teacherId, Guid classId);
        Task<TeacherClassResponseDto?> AddTeacherClassAsync(TeacherClassDto teacherClassDto, Guid userId);
        Task<TeacherClassResponseDto?> UpdateTeacherClassAsync(Guid id, TeacherClassDto teacherClassDto, Guid userId);
        Task<bool> RemoveTeacherClassAsync(Guid id, Guid userId);
        Task<bool> TeacherTeachesInClassAsync(Guid teacherId, Guid classId);
        Task<bool> TeacherTeachesSubjectAsync(Guid teacherId, Guid subjectId);
        Task<IEnumerable<TeacherClassResponseDto>> GetByTeacherAndSubjectAsync(Guid teacherId, Guid subjectId);
    }
}
