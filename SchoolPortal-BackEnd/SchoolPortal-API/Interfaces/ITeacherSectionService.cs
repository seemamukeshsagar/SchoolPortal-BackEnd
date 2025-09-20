using SchoolPortal_API.ViewModels.TeacherSection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherSectionService
    {
        Task<IEnumerable<TeacherSectionResponseDto>> GetAllTeacherSectionsAsync();
        Task<TeacherSectionResponseDto?> GetTeacherSectionByIdAsync(Guid id);
        Task<IEnumerable<TeacherSectionResponseDto>> GetSectionsByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherSectionResponseDto>> GetTeachersBySectionIdAsync(Guid sectionId);
        Task<TeacherSectionResponseDto?> GetClassTeacherAsync(Guid classId, Guid sectionId);
        Task<TeacherSectionResponseDto?> AddTeacherSectionAsync(TeacherSectionDto teacherSectionDto, Guid userId);
        Task<TeacherSectionResponseDto?> UpdateTeacherSectionAsync(Guid id, TeacherSectionDto teacherSectionDto, Guid userId);
        Task<bool> RemoveTeacherSectionAsync(Guid id, Guid userId);
        Task<bool> IsClassTeacherAsync(Guid teacherId, Guid classId, Guid sectionId);
        Task<bool> TeacherTeachesInSectionAsync(Guid teacherId, Guid sectionId);
        Task<bool> SetClassTeacherAsync(Guid teacherId, Guid classId, Guid sectionId, Guid userId);
    }
}
