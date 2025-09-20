using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherSectionRepository : IRepository<TeacherSectionDetail>
    {
        Task<IEnumerable<TeacherSectionDetail>> GetByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherSectionDetail>> GetByClassIdAsync(Guid classId);
        Task<IEnumerable<TeacherSectionDetail>> GetBySectionIdAsync(Guid sectionId);
        Task<IEnumerable<TeacherSectionDetail>> GetBySubjectIdAsync(Guid subjectId);
        Task<IEnumerable<TeacherSectionDetail>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<TeacherSectionDetail>> GetByCompanyIdAsync(Guid companyId);
        Task<TeacherSectionDetail?> GetClassTeacherAsync(Guid classId, Guid sectionId);
        Task<bool> IsClassTeacherAsync(Guid teacherId, Guid classId, Guid sectionId);
        Task<bool> TeacherTeachesInSectionAsync(Guid teacherId, Guid sectionId);
    }
}
