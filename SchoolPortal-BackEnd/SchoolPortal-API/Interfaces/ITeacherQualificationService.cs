using SchoolPortal_API.ViewModels.TeacherQualification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherQualificationService
    {
        Task<IEnumerable<TeacherQualificationResponseDto>> GetAllQualificationsAsync();
        Task<TeacherQualificationResponseDto?> GetQualificationByIdAsync(Guid id);
        Task<IEnumerable<TeacherQualificationResponseDto>> GetQualificationsByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherQualificationResponseDto>> GetActiveQualificationsByTeacherIdAsync(Guid teacherId);
        Task<TeacherQualificationResponseDto?> AddQualificationAsync(TeacherQualificationDto qualificationDto, Guid userId);
        Task<TeacherQualificationResponseDto?> UpdateQualificationAsync(Guid id, TeacherQualificationDto qualificationDto, Guid userId);
        Task<bool> RemoveQualificationAsync(Guid id, Guid userId);
        Task<bool> TeacherHasQualificationAsync(Guid teacherId, Guid qualificationId);
    }
}
