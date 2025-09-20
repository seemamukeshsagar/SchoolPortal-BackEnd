using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherDocumentRepository : IRepository<TeacherDocumentDetail>
    {
        Task<IEnumerable<TeacherDocumentDetail>> GetByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherDocumentDetail>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<TeacherDocumentDetail>> GetByCompanyIdAsync(Guid companyId);
        Task<TeacherDocumentDetail?> GetByFileNameAsync(string fileName);
        Task<bool> FileNameExistsAsync(string fileName, Guid? excludeId = null);
    }
}
