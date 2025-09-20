using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherQualificationRepository : IRepository<TeacherQualificationDetail>
    {
        Task<IEnumerable<TeacherQualificationDetail>> GetByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherQualificationDetail>> GetByQualificationIdAsync(Guid qualificationId);
        Task<bool> TeacherHasQualificationAsync(Guid teacherId, Guid qualificationId);
        Task<IEnumerable<TeacherQualificationDetail>> GetActiveQualificationsByTeacherIdAsync(Guid teacherId);
    }
}
