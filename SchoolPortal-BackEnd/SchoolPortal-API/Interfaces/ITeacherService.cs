using SchoolPortal_API.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherResponseDto>> GetAllTeachersAsync();
        Task<TeacherResponseDto?> GetTeacherByIdAsync(Guid id);
        Task<TeacherResponseDto?> CreateTeacherAsync(TeacherDto teacherDto, Guid userId);
        Task<TeacherResponseDto?> UpdateTeacherAsync(Guid id, TeacherDto teacherDto, Guid userId);
        Task<bool> DeleteTeacherAsync(Guid id, Guid userId);
        Task<IEnumerable<TeacherResponseDto>> GetTeachersBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<TeacherResponseDto>> GetTeachersByCompanyIdAsync(Guid companyId);
        Task<TeacherResponseDto?> GetTeacherByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
        Task<IEnumerable<TeacherResponseDto>> SearchTeachersAsync(string searchTerm, Guid? companyId = null, Guid? schoolId = null);
    }
}
