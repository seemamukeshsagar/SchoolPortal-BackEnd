using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class TeacherClassRepository : Repository<TeacherClassDetail>, ITeacherClassRepository
    {
        public TeacherClassRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeacherClassDetail>> GetByTeacherIdAsync(Guid teacherId)
        {
            return await _context.TeacherClassDetails
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Class)
                .Include(tc => tc.Section)
                .Include(tc => tc.Subject)
                .Where(tc => tc.TeacherId == teacherId && !tc.IsDeleted)
                .OrderBy(tc => tc.Class.Name)
                .ThenBy(tc => tc.Section.Name)
                .ThenBy(tc => tc.Subject.SubjectName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherClassDetail>> GetByClassIdAsync(Guid classId)
        {
            return await _context.TeacherClassDetails
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Class)
                .Include(tc => tc.Section)
                .Include(tc => tc.Subject)
                .Where(tc => tc.ClassId == classId && !tc.IsDeleted)
                .OrderBy(tc => tc.Teacher.FirstName)
                .ThenBy(tc => tc.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherClassDetail>> GetBySectionIdAsync(Guid sectionId)
        {
            return await _context.TeacherClassDetails
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Class)
                .Include(tc => tc.Section)
                .Include(tc => tc.Subject)
                .Where(tc => tc.SectionId == sectionId && !tc.IsDeleted)
                .OrderBy(tc => tc.Class.Name)
                .ThenBy(tc => tc.Teacher.FirstName)
                .ThenBy(tc => tc.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherClassDetail>> GetBySubjectIdAsync(Guid subjectId)
        {
            return await _context.TeacherClassDetails
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Class)
                .Include(tc => tc.Section)
                .Include(tc => tc.Subject)
                .Where(tc => tc.SubjectId == subjectId && !tc.IsDeleted)
                .OrderBy(tc => tc.Class.Name)
                .ThenBy(tc => tc.Section.Name)
                .ThenBy(tc => tc.Teacher.FirstName)
                .ThenBy(tc => tc.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherClassDetail>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.TeacherClassDetails
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Class)
                .Include(tc => tc.Section)
                .Include(tc => tc.Subject)
                .Where(tc => tc.SchoolId == schoolId && !tc.IsDeleted)
                .OrderBy(tc => tc.Class.Name)
                .ThenBy(tc => tc.Section.Name)
                .ThenBy(tc => tc.Teacher.FirstName)
                .ThenBy(tc => tc.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherClassDetail>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.TeacherClassDetails
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Class)
                .Include(tc => tc.Section)
                .Include(tc => tc.Subject)
                .Where(tc => tc.CompanyId == companyId && !tc.IsDeleted)
                .OrderBy(tc => tc.School.Name)
                .ThenBy(tc => tc.Class.Name)
                .ThenBy(tc => tc.Section.Name)
                .ThenBy(tc => tc.Teacher.FirstName)
                .ThenBy(tc => tc.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<bool> TeacherTeachesInClassAsync(Guid teacherId, Guid classId)
        {
            return await _context.TeacherClassDetails
                .AnyAsync(tc => tc.TeacherId == teacherId && 
                              tc.ClassId == classId && 
                              tc.IsActive && 
                              !tc.IsDeleted);
        }

        public async Task<bool> TeacherTeachesSubjectAsync(Guid teacherId, Guid subjectId)
        {
            return await _context.TeacherClassDetails
                .AnyAsync(tc => tc.TeacherId == teacherId && 
                              tc.SubjectId == subjectId && 
                              tc.IsActive && 
                              !tc.IsDeleted);
        }

        public override async Task<TeacherClassDetail?> GetByIdAsync(Guid id)
        {
            return await _context.TeacherClassDetails
                .Include(tc => tc.Teacher)
                .Include(tc => tc.Class)
                .Include(tc => tc.Section)
                .Include(tc => tc.Subject)
                .Include(tc => tc.Company)
                .Include(tc => tc.School)
                .FirstOrDefaultAsync(tc => tc.Id == id && !tc.IsDeleted);
        }
    }
}
