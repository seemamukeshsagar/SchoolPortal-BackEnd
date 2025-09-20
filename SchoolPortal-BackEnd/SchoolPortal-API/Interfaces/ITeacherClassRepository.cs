using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherClassRepository : IRepository<TeacherClassDetail>
    {
        Task<IEnumerable<TeacherClassDetail>> GetByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherClassDetail>> GetByClassIdAsync(Guid classId);
        Task<IEnumerable<TeacherClassDetail>> GetBySectionIdAsync(Guid sectionId);
        Task<IEnumerable<TeacherClassDetail>> GetBySubjectIdAsync(Guid subjectId);
        Task<IEnumerable<TeacherClassDetail>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<TeacherClassDetail>> GetByCompanyIdAsync(Guid companyId);
        Task<bool> TeacherTeachesInClassAsync(Guid teacherId, Guid classId);
        Task<bool> TeacherTeachesSubjectAsync(Guid teacherId, Guid subjectId);
    }
}
