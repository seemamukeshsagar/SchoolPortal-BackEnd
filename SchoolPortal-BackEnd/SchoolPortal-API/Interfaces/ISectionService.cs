using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal_API.ViewModels.Section;

namespace SchoolPortal_API.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionResponseDto>> GetAllAsync();
        Task<SectionResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<SectionResponseDto>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<SectionResponseDto>> GetByCompanyIdAsync(Guid companyId);
        Task<SectionResponseDto?> CreateAsync(SectionDto dto, Guid userId);
        Task<SectionResponseDto?> UpdateAsync(Guid id, SectionDto dto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
