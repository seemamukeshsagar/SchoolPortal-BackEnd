using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class SchoolRepository : Repository<SchoolMaster>, ISchoolRepository
    {
        public SchoolRepository(SchoolPortalContext context) : base(context)
        {
        }

        public async Task<SchoolMaster?> GetSchoolWithDetailsAsync(Guid id)
        {
            return await _context.SchoolMasters
                .Include(s => s.Company)
                .Include(s => s.Country)
                .Include(s => s.State)
                .Include(s => s.City)
                .Include(s => s.JudistrictionCountry)
                .Include(s => s.JudistrictionState)
                .Include(s => s.JudistrictionCity)
                .Include(s => s.BankCountry)
                .Include(s => s.BankState)
                .Include(s => s.BankCity)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SchoolMaster>> GetSchoolsByCompanyIdAsync(Guid companyId)
        {
            return await _context.SchoolMasters
                .Where(s => s.CompanyId == companyId && !s.IsDeleted)
                .Include(s => s.Company)
                .Include(s => s.Country)
                .Include(s => s.State)
                .Include(s => s.City)
                .ToListAsync();
        }
    }
}
