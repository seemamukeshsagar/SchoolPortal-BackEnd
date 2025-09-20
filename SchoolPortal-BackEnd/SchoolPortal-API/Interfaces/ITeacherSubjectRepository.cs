using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherSubjectRepository : IRepository<TeacherSubjectDetail>
    {
        Task<IEnumerable<TeacherSubjectDetail>> GetByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherSubjectDetail>> GetBySubjectIdAsync(Guid subjectId);
        Task<IEnumerable<TeacherSubjectDetail>> GetByClassIdAsync(Guid classId);
        Task<IEnumerable<TeacherSubjectDetail>> GetBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<TeacherSubjectDetail>> GetByCompanyIdAsync(Guid companyId);
        Task<bool> IsTeacherAssignedToSubjectAsync(Guid teacherId, Guid subjectId, Guid classId);
        Task<IEnumerable<TeacherSubjectDetail>> GetByTeacherAndClassAsync(Guid teacherId, Guid classId);
    }
}
