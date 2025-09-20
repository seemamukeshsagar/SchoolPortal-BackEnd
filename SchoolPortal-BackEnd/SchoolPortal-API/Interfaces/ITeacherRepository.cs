using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Shared.Models;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherRepository : IRepository<TeacherMaster>
    {
        Task<IEnumerable<TeacherMaster>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<TeacherMaster>> GetByCompanyIdAsync(Guid companyId);
        Task<TeacherMaster?> GetByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null);
        Task<IEnumerable<TeacherMaster>> SearchTeachersAsync(string searchTerm, Guid? companyId = null, Guid? schoolId = null);
    }
}
