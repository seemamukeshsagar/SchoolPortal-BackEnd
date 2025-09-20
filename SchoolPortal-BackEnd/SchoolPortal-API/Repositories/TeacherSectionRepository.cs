using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class TeacherSectionRepository : Repository<TeacherSectionDetail>, ITeacherSectionRepository
    {
        public TeacherSectionRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeacherSectionDetail>> GetByTeacherIdAsync(Guid teacherId)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Class)
                .Include(ts => ts.Section)
                .Include(ts => ts.Subject)
                .Where(ts => ts.TeacherId == teacherId && !ts.IsDeleted)
                .OrderBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Section.Name)
                .ThenBy(ts => ts.Subject.SubjectName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSectionDetail>> GetByClassIdAsync(Guid classId)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Class)
                .Include(ts => ts.Section)
                .Include(ts => ts.Subject)
                .Where(ts => ts.ClassId == classId && !ts.IsDeleted)
                .OrderBy(ts => ts.Section.Name)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSectionDetail>> GetBySectionIdAsync(Guid sectionId)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Class)
                .Include(ts => ts.Section)
                .Include(ts => ts.Subject)
                .Where(ts => ts.SectionId == sectionId && !ts.IsDeleted)
                .OrderBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Subject.SubjectName)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSectionDetail>> GetBySubjectIdAsync(Guid subjectId)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Class)
                .Include(ts => ts.Section)
                .Include(ts => ts.Subject)
                .Where(ts => ts.SubjectId == subjectId && !ts.IsDeleted)
                .OrderBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Section.Name)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSectionDetail>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Class)
                .Include(ts => ts.Section)
                .Include(ts => ts.Subject)
                .Where(ts => ts.SchoolId == schoolId && !ts.IsDeleted)
                .OrderBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Section.Name)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSectionDetail>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Class)
                .Include(ts => ts.Section)
                .Include(ts => ts.Subject)
                .Include(ts => ts.School)
                .Where(ts => ts.CompanyId == companyId && !ts.IsDeleted)
                .OrderBy(ts => ts.School.Name)
                .ThenBy(ts => ts.Class.Name)
                .ThenBy(ts => ts.Section.Name)
                .ThenBy(ts => ts.Teacher.FirstName)
                .ThenBy(ts => ts.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<TeacherSectionDetail?> GetClassTeacherAsync(Guid classId, Guid sectionId)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .FirstOrDefaultAsync(ts => ts.ClassId == classId && 
                                         ts.SectionId == sectionId && 
                                         ts.IsClassTeacher && 
                                         ts.IsActive && 
                                         !ts.IsDeleted);
        }

        public async Task<bool> IsClassTeacherAsync(Guid teacherId, Guid classId, Guid sectionId)
        {
            return await _context.TeacherSectionDetails
                .AnyAsync(ts => ts.TeacherId == teacherId && 
                              ts.ClassId == classId && 
                              ts.SectionId == sectionId && 
                              ts.IsClassTeacher && 
                              ts.IsActive && 
                              !ts.IsDeleted);
        }

        public async Task<bool> TeacherTeachesInSectionAsync(Guid teacherId, Guid sectionId)
        {
            return await _context.TeacherSectionDetails
                .AnyAsync(ts => ts.TeacherId == teacherId && 
                              ts.SectionId == sectionId && 
                              ts.IsActive && 
                              !ts.IsDeleted);
        }

        public override async Task<TeacherSectionDetail?> GetByIdAsync(Guid id)
        {
            return await _context.TeacherSectionDetails
                .Include(ts => ts.Teacher)
                .Include(ts => ts.Class)
                .Include(ts => ts.Section)
                .Include(ts => ts.Subject)
                .Include(ts => ts.School)
                .Include(ts => ts.Company)
                .FirstOrDefaultAsync(ts => ts.Id == id && !ts.IsDeleted);
        }
    }
}
