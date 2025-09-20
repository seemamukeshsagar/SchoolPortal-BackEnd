using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class SchoolRepository : Repository<SchoolMaster>, ISchoolRepository
    {
        public SchoolRepository(SchoolNewPortalContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<SchoolMaster>> GetAllAsync()
        {
            return await _context.SchoolMasters
                .Where(s => !s.IsDeleted)
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
                .ToListAsync();
        }

        public override async Task<SchoolMaster?> GetByIdAsync(Guid id)
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
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<SchoolMaster?> GetSchoolWithDetailsAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<SchoolMaster>> GetSchoolsByCompanyIdAsync(Guid companyId)
        {
            return await _context.SchoolMasters
                .Where(s => s.CompanyId == companyId && !s.IsDeleted)
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
                .ToListAsync();
        }

        public async Task<SchoolMaster> CreateAsync(SchoolMaster school, Guid userId)
        {
            school.Id = Guid.NewGuid();
            school.CreatedBy = userId;
            school.CreatedDate = DateTime.UtcNow;
            school.IsDeleted = false;
            school.Status = "Active";

            await _dbSet.AddAsync(school);
            await _context.SaveChangesAsync();
            return school;
        }

        public async Task<bool> UpdateAsync(SchoolMaster school, Guid userId)
        {
            var existingSchool = await GetByIdAsync(school.Id);
            if (existingSchool == null)
                return false;

            school.ModifiedBy = userId;
            school.ModifiedDate = DateTime.UtcNow;

            _context.Entry(existingSchool).CurrentValues.SetValues(school);
            _context.Entry(existingSchool).State = EntityState.Modified;
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var school = await GetByIdAsync(id);
            if (school == null)
                return false;

            school.IsDeleted = true;
            school.ModifiedBy = userId;
            school.ModifiedDate = DateTime.UtcNow;

            _context.Entry(school).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> HardDeleteAsync(Guid id)
        {
            var school = await GetByIdAsync(id);
            if (school == null)
                return false;

            _dbSet.Remove(school);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<SchoolMaster, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<SchoolMaster, bool>> predicate = null)
        {
            return predicate == null 
                ? await _dbSet.CountAsync() 
                : await _dbSet.CountAsync(predicate);
        }

        public async Task<IEnumerable<SchoolMaster>> FindAsync(
            Expression<Func<SchoolMaster, bool>> predicate = null,
            Func<IQueryable<SchoolMaster>, IOrderedQueryable<SchoolMaster>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<SchoolMaster> query = _context.SchoolMasters
                .Where(s => !s.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<int> CountByCityIdAsync(Guid cityId)
        {
            return await _context.SchoolMasters
                .Where(s => s.CityId == cityId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByJurisdictionCityIdAsync(Guid jurisdictionCityId)
        {
            return await _context.SchoolMasters
                .Where(s => s.JudistrictionCityId == jurisdictionCityId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByBankCityIdAsync(Guid bankCityId)
        {
            return await _context.SchoolMasters
                .Where(s => s.BankCityId == bankCityId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByStateIdAsync(Guid stateId)
        {
            return await _context.SchoolMasters
                .Where(s => s.StateId == stateId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByJurisdictionStateIdAsync(Guid jurisdictionStateId)
        {
            return await _context.SchoolMasters
                .Where(s => s.JudistrictionStateId == jurisdictionStateId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByBankStateIdAsync(Guid bankStateId)
        {
            return await _context.SchoolMasters
                .Where(s => s.BankStateId == bankStateId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByCountryIdAsync(Guid countryId)
        {
            return await _context.SchoolMasters
                .Where(s => s.CountryId == countryId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByJurisdictionCountryIdAsync(Guid jurisdictionCountryId)
        {
            return await _context.SchoolMasters
                .Where(s => s.JudistrictionCountryId == jurisdictionCountryId && !s.IsDeleted)
                .CountAsync();
        }

        public async Task<int> CountByBankCountryIdAsync(Guid bankCountryId)
        {
            return await _context.SchoolMasters
                .Where(s => s.BankCountryId == bankCountryId && !s.IsDeleted)
                .CountAsync();
        }
    }
}
