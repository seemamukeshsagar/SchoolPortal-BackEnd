using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class SectionRepository : Repository<SectionMaster>, ISectionRepository
    {
        public SectionRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<SectionMaster>> GetAllAsync()
        {
            return await _context.SectionMasters
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public override async Task<SectionMaster?> GetByIdAsync(Guid id)
        {
            return await _context.SectionMasters
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<IEnumerable<SectionMaster>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.SectionMasters
                .Where(s => s.SchoolId == schoolId && !s.IsDeleted)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<SectionMaster>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.SectionMasters
                .Where(s => s.CompanyId == companyId && !s.IsDeleted)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<bool> NameExistsAsync(string name, Guid schoolId, Guid companyId, Guid? excludeId = null)
        {
            var q = _context.SectionMasters
                .Where(s => !s.IsDeleted && s.SchoolId == schoolId && s.CompanyId == companyId && s.Name.ToLower() == name.ToLower());
            if (excludeId.HasValue)
            {
                q = q.Where(s => s.Id != excludeId.Value);
            }
            return await q.AnyAsync();
        }
    }
}
