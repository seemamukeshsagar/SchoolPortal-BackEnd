using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class CompanyRepository : Repository<CompanyMaster>, ICompanyRepository
    {
        public CompanyRepository(SchoolPortalContext context) : base(context)
        {
        }

        public async Task<IEnumerable<dynamic>> GetCountriesAsync()
        {
            return await _context.CountryMasters
                .OrderBy(c => c.CountryName)
                .Select(c => new { c.Id, c.CountryName })
                .ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetStatesByCountryIdAsync(Guid countryId)
        {
            return await _context.StateMasters
                .Where(s => s.CountryId == countryId)
                .OrderBy(s => s.StateName)
                .Select(s => new { s.Id, s.StateName })
                .ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetCitiesByStateIdAsync(Guid stateId)
        {
            return await _context.CityMasters
                .Where(c => c.CityStateId == stateId)
                .OrderBy(c => c.CityName)
                .Select(c => new { c.Id, c.CityName })
                .ToListAsync();
        }

        public async Task<CompanyMaster?> GetCompanyWithDetailsAsync(Guid id)
        {
            return await _context.CompanyMasters
                .Include(c => c.Country)
                .Include(c => c.State)
                .Include(c => c.City)
                .Include(c => c.JudistrictionAreaNavigation)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
