using SchoolPortal_API.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ISchoolService
    {
        Task<IEnumerable<SchoolResponseDto>> GetAllSchoolsAsync();
        Task<IEnumerable<SchoolResponseDto>> GetSchoolsByCompanyIdAsync(Guid companyId);
        Task<SchoolResponseDto?> GetSchoolByIdAsync(Guid id);
        Task<SchoolResponseDto?> CreateSchoolAsync(SchoolDto schoolDto, Guid userId);
        Task<SchoolResponseDto?> UpdateSchoolAsync(Guid id, SchoolDto schoolDto, Guid userId);
        Task<bool> DeleteSchoolAsync(Guid id, Guid userId);
    }
}
