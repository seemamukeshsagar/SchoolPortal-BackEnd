using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal_API.ViewModels.ClassSection;

namespace SchoolPortal_API.Interfaces
{
    public interface IClassSectionService
    {
        Task<IEnumerable<ClassSectionResponseDto>> GetAllAsync();
        Task<ClassSectionResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ClassSectionResponseDto>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<ClassSectionResponseDto>> GetByCompanyIdAsync(Guid companyId);
        Task<IEnumerable<ClassSectionResponseDto>> GetByClassIdAsync(Guid classId);
        Task<IEnumerable<ClassSectionResponseDto>> GetBySectionIdAsync(Guid sectionId);
        Task<ClassSectionResponseDto?> CreateAsync(ClassSectionDto dto, Guid userId);
        Task<ClassSectionResponseDto?> UpdateAsync(Guid id, ClassSectionDto dto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
