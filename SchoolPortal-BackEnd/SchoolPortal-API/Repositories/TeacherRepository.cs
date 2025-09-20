using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class TeacherRepository : Repository<TeacherMaster>, ITeacherRepository
    {
        public TeacherRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TeacherMaster>> GetBySchoolIdAsync(Guid schoolId)
        {
            return await _context.TeacherMasters
                .Where(t => t.SchoolId == schoolId && !t.IsDeleted)
                .OrderBy(t => t.FirstName)
                .ThenBy(t => t.LastName)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherMaster>> GetByCompanyIdAsync(Guid companyId)
        {
            return await _context.TeacherMasters
                .Where(t => t.CompanyId == companyId && !t.IsDeleted)
                .OrderBy(t => t.FirstName)
                .ThenBy(t => t.LastName)
                .ToListAsync();
        }

        public async Task<TeacherMaster?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _context.TeacherMasters
                .FirstOrDefaultAsync(t => t.Email != null && t.Email.ToLower() == email.ToLower() && !t.IsDeleted);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;

            var query = _context.TeacherMasters
                .Where(t => t.Email != null && t.Email.ToLower() == email.ToLower() && !t.IsDeleted);

            if (excludeId.HasValue)
            {
                query = query.Where(t => t.Id != excludeId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task<IEnumerable<TeacherMaster>> SearchTeachersAsync(string searchTerm, Guid? companyId = null, Guid? schoolId = null)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<TeacherMaster>();
            }

            var query = _context.TeacherMasters
                .Where(t => !t.IsDeleted && 
                    (EF.Functions.Like(t.FirstName, $"%{searchTerm}%") ||
                     EF.Functions.Like(t.LastName, $"%{searchTerm}%") ||
                     (t.Email != null && EF.Functions.Like(t.Email, $"%{searchTerm}%")) ||
                     (t.Phone != null && EF.Functions.Like(t.Phone, $"%{searchTerm}%")) ||
                     (t.MobilePhone != null && EF.Functions.Like(t.MobilePhone, $"%{searchTerm}%"))));

            if (companyId.HasValue)
            {
                query = query.Where(t => t.CompanyId == companyId.Value);
            }

            if (schoolId.HasValue)
            {
                query = query.Where(t => t.SchoolId == schoolId.Value);
            }

            return await query
                .OrderBy(t => t.FirstName)
                .ThenBy(t => t.LastName)
                .ToListAsync();
        }

        public override async Task<TeacherMaster?> GetByIdAsync(Guid id)
        {
            return await _context.TeacherMasters
                .Include(t => t.City)
                .Include(t => t.State)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }
    }
}
