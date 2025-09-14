using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ISchoolRepository : IRepository<SchoolMaster>
    {
        Task<SchoolMaster?> GetSchoolWithDetailsAsync(Guid id);
        Task<IEnumerable<SchoolMaster>> GetSchoolsByCompanyIdAsync(Guid companyId);
    }
}
