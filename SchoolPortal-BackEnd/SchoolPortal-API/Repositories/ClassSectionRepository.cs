using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class ClassSectionRepository : Repository<ClassSectionDetail>, IClassSectionRepository
    {
        public ClassSectionRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<ClassSectionDetail>> GetAllAsync()
        {
            return await _context.ClassSectionDetails
                .Include(cs => cs.Class)
                .Include(cs => cs.Section)
                .Where(cs => !cs.IsDeleted)
                .OrderBy(cs => cs.CreatedDate)
                .ToListAsync();
        }

        public override async Task<ClassSectionDetail?> GetByIdAsync(Guid id)
        {
            return await _context.ClassSectionDetails
                .Include(cs => cs.Class)
                .Include(cs => cs.Section)
                .FirstOrDefaultAsync(cs => cs.Id == id && !cs.IsDeleted);
        }

        public async Task<IEnumerable<ClassSectionDetail>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.ClassSectionDetails
                .Include(cs => cs.Class)
                .Include(cs => cs.Section)
                .Where(cs => cs.SchoolId == schoolId && !cs.IsDeleted)
                .OrderBy(cs => cs.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassSectionDetail>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.ClassSectionDetails
                .Include(cs => cs.Class)
                .Include(cs => cs.Section)
                .Where(cs => cs.CompanyId == companyId && !cs.IsDeleted)
                .OrderBy(cs => cs.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassSectionDetail>> GetByClassIdAsync(Guid classId)
        {
            return await _context.ClassSectionDetails
                .Include(cs => cs.Class)
                .Include(cs => cs.Section)
                .Where(cs => cs.ClassMasterId == classId && !cs.IsDeleted)
                .OrderBy(cs => cs.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassSectionDetail>> GetBySectionIdAsync(Guid sectionId)
        {
            return await _context.ClassSectionDetails
                .Include(cs => cs.Class)
                .Include(cs => cs.Section)
                .Where(cs => cs.SectionMasterId == sectionId && !cs.IsDeleted)
                .OrderBy(cs => cs.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid classId, Guid sectionId, Guid schoolId, Guid companyId, Guid? excludeId = null)
        {
            var q = _context.ClassSectionDetails
                .Where(cs => !cs.IsDeleted && cs.ClassMasterId == classId && cs.SectionMasterId == sectionId && cs.SchoolId == schoolId && cs.CompanyId == companyId);
            if (excludeId.HasValue)
            {
                q = q.Where(cs => cs.Id != excludeId.Value);
            }
            return await q.AnyAsync();
        }
    }
}
