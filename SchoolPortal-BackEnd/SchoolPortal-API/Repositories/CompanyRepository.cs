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
        public CompanyRepository(SchoolNewPortalContext context) : base(context)
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

        public async Task<int> CountByCityIdAsync(Guid cityId)
        {
            return await _context.CompanyMasters
                .Where(c => c.CityId == cityId && !c.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByJurisdictionCityIdAsync(Guid jurisdictionCityId)
        {
            return await _context.CompanyMasters
                .Where(c => c.JudistrictionArea == jurisdictionCityId && !c.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByStateIdAsync(Guid stateId)
        {
            return await _context.CompanyMasters
                .Where(c => c.StateId == stateId && !c.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByJurisdictionStateIdAsync(Guid jurisdictionStateId)
        {
            return await _context.CompanyMasters
                .Where(c => c.JudistrictionArea == jurisdictionStateId && !c.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByCountryIdAsync(Guid countryId)
        {
            return await _context.CompanyMasters
                .Where(c => c.CountryId == countryId && !c.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByJurisdictionCountryIdAsync(Guid jurisdictionCountryId)
        {
            return await _context.CompanyMasters
                .Where(c => c.CountryId == jurisdictionCountryId && !c.IsDeleted)
                .CountAsync();
        }

    }
}
