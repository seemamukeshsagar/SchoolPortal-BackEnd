using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ISectionRepository : IRepository<SectionMaster>
    {
        Task<IEnumerable<SectionMaster>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<SectionMaster>> GetByCompanyIdAsync(Guid companyId);
        Task<bool> NameExistsAsync(string name, Guid schoolId, Guid companyId, Guid? excludeId = null);
    }
}
