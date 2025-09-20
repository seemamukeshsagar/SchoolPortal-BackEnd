using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class TeacherDocumentRepository : Repository<TeacherDocumentDetail>, ITeacherDocumentRepository
    {
        public TeacherDocumentRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeacherDocumentDetail>> GetByTeacherIdAsync(Guid teacherId)
        {
            return await _context.TeacherDocumentDetails
                .Where(d => d.TeacherId == teacherId && !d.IsDeleted)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherDocumentDetail>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.TeacherDocumentDetails
                .Where(d => d.SchoolId == schoolId && !d.IsDeleted)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherDocumentDetail>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.TeacherDocumentDetails
                .Where(d => d.CompanyId == companyId && !d.IsDeleted)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        public async Task<TeacherDocumentDetail?> GetByFileNameAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            return await _context.TeacherDocumentDetails
                .FirstOrDefaultAsync(d => d.FileName != null && d.FileName.ToLower() == fileName.ToLower() && !d.IsDeleted);
        }

        public async Task<bool> FileNameExistsAsync(string fileName, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var query = _context.TeacherDocumentDetails
                .Where(d => d.FileName != null && d.FileName.ToLower() == fileName.ToLower() && !d.IsDeleted);

            if (excludeId.HasValue)
            {
                query = query.Where(d => d.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public override async Task<TeacherDocumentDetail?> GetByIdAsync(Guid id)
        {
            return await _context.TeacherDocumentDetails
                .Include(d => d.Teacher)
                .Include(d => d.Company)
                .Include(d => d.School)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }
    }
}
