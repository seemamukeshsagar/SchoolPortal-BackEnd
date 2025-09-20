using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal_API.ViewModels.Class;

namespace SchoolPortal_API.Interfaces
{
    public interface IClassService
    {
        Task<IEnumerable<ClassResponseDto>> GetAllAsync();
        Task<ClassResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ClassResponseDto>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<ClassResponseDto>> GetByCompanyIdAsync(Guid companyId);
        Task<ClassResponseDto?> CreateAsync(ClassDto dto, Guid userId);
        Task<ClassResponseDto?> UpdateAsync(Guid id, ClassDto dto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
