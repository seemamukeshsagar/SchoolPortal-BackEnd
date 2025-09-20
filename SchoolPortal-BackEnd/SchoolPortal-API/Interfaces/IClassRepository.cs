using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface IClassRepository : IRepository<ClassMaster>
    {
        Task<IEnumerable<ClassMaster>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<ClassMaster>> GetByCompanyIdAsync(Guid companyId);
        Task<bool> NameExistsAsync(string name, Guid schoolId, Guid companyId, Guid? excludeId = null);
    }
}
