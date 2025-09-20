using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class TeacherSubjectRepository : Repository<TeacherSubjectDetail>, ITeacherSubjectRepository
    {
        public TeacherSubjectRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeacherSubjectDetail>> GetByTeacherIdAsync(Guid teacherId)
        {
            return await _context.TeacherSubjectDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .Where(ts => ts.TeacherId == teacherId && !ts.IsDeleted)
                .OrderBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Subject.SubjectName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSubjectDetail>> GetBySubjectIdAsync(Guid subjectId)
        {
            return await _context.TeacherSubjectDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .Where(ts => ts.SubjectId == subjectId && !ts.IsDeleted)
                .OrderBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSubjectDetail>> GetByClassIdAsync(Guid classId)
        {
            return await _context.TeacherSubjectDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .Where(ts => ts.ClassMasterId == classId && !ts.IsDeleted)
                .OrderBy(ts => ts.Subject.SubjectName)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSubjectDetail>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.TeacherSubjectDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .Where(ts => ts.SchoolId == schoolId && !ts.IsDeleted)
                .OrderBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Subject.SubjectName)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSubjectDetail>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.TeacherSubjectDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .Include(ts => ts.School)
                .Where(ts => ts.CompanyId == companyId && !ts.IsDeleted)
                .OrderBy(ts => ts.School.Name)
                .ThenBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Subject.SubjectName)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<bool> IsTeacherAssignedToSubjectAsync(Guid teacherId, Guid subjectId, Guid classId)
        {
            return await _context.TeacherSubjectDetails
                .AnyAsync(ts => ts.TeacherId == teacherId && 
                              ts.SubjectId == subjectId && 
                              ts.ClassMasterId == classId && 
                              ts.IsActive && 
                              !ts.IsDeleted);
        }

        public async Task<IEnumerable<TeacherSubjectDetail>> GetByTeacherAndClassAsync(Guid teacherId, Guid classId)
        {
            return await _context.TeacherSubjectDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .Where(ts => ts.TeacherId == teacherId && 
                            ts.ClassMasterId == classId && 
                            !ts.IsDeleted)
                .OrderBy(ts => ts.Subject.SubjectName)
                .ToListAsync();
        }

        public override async Task<TeacherSubjectDetail?> GetByIdAsync(Guid id)
        {
            return await _context.TeacherSubjectDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Subject)
                .Include(ts => ts.Class)
                .Include(ts => ts.School)
                .Include(ts => ts.Company)
                .FirstOrDefaultAsync(ts => ts.Id == id && !ts.IsDeleted);
        }
    }
}
