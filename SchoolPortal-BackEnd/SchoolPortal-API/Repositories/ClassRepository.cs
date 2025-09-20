using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class ClassRepository : Repository<ClassMaster>, IClassRepository
    {
        public ClassRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<ClassMaster>> GetAllAsync()
        {
            return await _context.ClassMasters
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.OrderBy)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<ClassMaster?> GetByIdAsync(Guid id)
        {
            return await _context.ClassMasters
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<ClassMaster>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.ClassMasters
                .Where(c => c.SchoolId == schoolId && !c.IsDeleted)
                .OrderBy(c => c.OrderBy)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassMaster>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.ClassMasters
                .Where(c => c.CompanyId == companyId && !c.IsDeleted)
                .OrderBy(c => c.OrderBy)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<bool> NameExistsAsync(string name, Guid schoolId, Guid companyId, Guid? excludeId = null)
        {
            var q = _context.ClassMasters
                .Where(c => !c.IsDeleted && c.SchoolId == schoolId && c.CompanyId == companyId && c.Name.ToLower() == name.ToLower());
            if (excludeId.HasValue)
            {
                q = q.Where(c => c.Id != excludeId.Value);
            }
            return await q.AnyAsync();
        }
    }
}
