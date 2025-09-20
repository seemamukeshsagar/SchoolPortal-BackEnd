using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class TeacherQualificationRepository : Repository<TeacherQualificationDetail>, ITeacherQualificationRepository
    {
        public TeacherQualificationRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeacherQualificationDetail>> GetByTeacherIdAsync(Guid teacherId)
        {
            return await _context.TeacherQualificationDetails
                .Include(tq => tq.Qualification)
                .Where(tq => tq.TeacherId == teacherId && !tq.IsDeleted)
                .OrderBy(tq => tq.Qualification.QualificationName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherQualificationDetail>> GetByQualificationIdAsync(Guid qualificationId)
        {
            return await _context.TeacherQualificationDetails
                .Include(tq => tq.Teacher)
                .Where(tq => tq.QualifcationId == qualificationId && !tq.IsDeleted)
                .OrderBy(tq => tq.Teacher.FirstName)
                .ThenBy(tq => tq.Teacher.LastName)
                .ToListAsync();
        }

        public async Task<bool> TeacherHasQualificationAsync(Guid teacherId, Guid qualificationId)
        {
            return await _context.TeacherQualificationDetails
                .AnyAsync(tq => tq.TeacherId == teacherId && 
                              tq.QualifcationId == qualificationId && 
                              !tq.IsDeleted);
        }

        public async Task<IEnumerable<TeacherQualificationDetail>> GetActiveQualificationsByTeacherIdAsync(Guid teacherId)
        {
            return await _context.TeacherQualificationDetails
                .Include(tq => tq.Qualification)
                .Where(tq => tq.TeacherId == teacherId && 
                           tq.IsActive && 
                           !tq.IsDeleted)
                .OrderBy(tq => tq.Qualification.QualificationName)
                .ToListAsync();
        }

        public override async Task<TeacherQualificationDetail?> GetByIdAsync(Guid id)
        {
            return await _context.TeacherQualificationDetails
                .Include(tq => tq.Teacher)
                .Include(tq => tq.Qualification)
                .FirstOrDefaultAsync(tq => tq.Id == id && !tq.IsDeleted);
        }
    }
}
