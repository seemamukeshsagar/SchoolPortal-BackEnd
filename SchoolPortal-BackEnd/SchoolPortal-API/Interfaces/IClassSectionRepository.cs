using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface IClassSectionRepository : IRepository<ClassSectionDetail>
    {
        Task<IEnumerable<ClassSectionDetail>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<ClassSectionDetail>> GetByCompanyIdAsync(Guid companyId);
        Task<IEnumerable<ClassSectionDetail>> GetByClassIdAsync(Guid classId);
        Task<IEnumerable<ClassSectionDetail>> GetBySectionIdAsync(Guid sectionId);
        Task<bool> ExistsAsync(Guid classId, Guid sectionId, Guid schoolId, Guid companyId, Guid? excludeId = null);
    }
}
