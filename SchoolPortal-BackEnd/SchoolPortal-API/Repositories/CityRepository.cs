using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;

namespace SchoolPortal_API.Repositories
{
    public class CityRepository : IRepository<CityMaster>, ICityRepository
    {
        private readonly SchoolPortalContext _context;

        public CityRepository(SchoolPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region IRepository Implementation
        
        public async Task<CityMaster?> GetByIdAsync(Guid id)
        {
            return await _context.CityMasters
                .Include(c => c.CityStateNavigation)
                .Include(c => c.CityStateNavigation.Country)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<CityMaster>> GetAllAsync()
        {
            return await _context.CityMasters
                .Include(c => c.CityStateNavigation)
                .Include(c => c.CityStateNavigation.Country)
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.CityName)
                .ToListAsync();
        }

        public async Task<IEnumerable<CityMaster>> FindAsync(Expression<Func<CityMaster, bool>> predicate)
        {
            return await _context.CityMasters
                .Include(c => c.CityStateNavigation)
                .Include(c => c.CityStateNavigation.Country)
                .Where(c => !c.IsDeleted)
                .Where(predicate)
                .OrderBy(c => c.CityName)
                .ToListAsync();
        }

        public async Task AddAsync(CityMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.CityMasters.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<CityMaster> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            await _context.CityMasters.AddRangeAsync(entities);
        }

        public void Update(CityMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(CityMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RemoveRange(IEnumerable<CityMaster> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<CityMaster, bool>> predicate)
        {
            return await _context.CityMasters
                .Where(c => !c.IsDeleted)
                .AnyAsync(predicate);
        }

        #endregion

        #region ICityRepository Implementation

        public async Task<(IEnumerable<CityMaster> items, int totalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? stateId = null,
            Guid? countryId = null,
            bool? isActive = null)
        {
            IQueryable<CityMaster> query = _context.CityMasters
                .Include(c => c.CityStateNavigation)
                .Include(c => c.CityStateNavigation.Country)
                .Where(c => !c.IsDeleted);

            if (stateId.HasValue)
            {
                query = query.Where(c => c.CityStateId == stateId.Value);
            }
            else if (countryId.HasValue)
            {
                query = query.Where(c => c.CityStateNavigation.CountryId == countryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.CityName.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                query = query.Where(c => c.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.CityName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<IEnumerable<CityMaster>> GetByStateIdAsync(Guid stateId, bool includeInactive = false)
        {
            IQueryable<CityMaster> query = _context.CityMasters
                .Include(c => c.CityStateNavigation)
                .Where(c => c.CityStateId == stateId && !c.IsDeleted);

            if (!includeInactive)
            {
                query = query.Where(c => c.IsActive);
            }

            return await query
                .OrderBy(c => c.CityName)
                .ToListAsync();
        }

        public async Task<IEnumerable<CityMaster>> GetByCountryIdAsync(Guid countryId, bool includeInactive = false)
        {
            IQueryable<CityMaster> query = _context.CityMasters
                .Include(c => c.CityStateNavigation)
                .Where(c => c.CityStateNavigation.CountryId == countryId && !c.IsDeleted);

            if (!includeInactive)
            {
                query = query.Where(c => c.IsActive);
            }

            return await query
                .OrderBy(c => c.CityName)
                .ToListAsync();
        }

        async Task<CityMaster?> ICityRepository.GetByIdAsync(Guid id)
        {
            var city = await GetByIdAsync(id);
            if (city == null)
                throw new KeyNotFoundException($"City with ID {id} not found");
            return city;
        }

        public async Task<CityMaster> CreateAsync(CityMaster entity, string userId)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(userId);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;

            await _context.CityMasters.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(CityMaster entity, string userId)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.ModifiedBy = Guid.Parse(userId);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Entry(entity).State = EntityState.Modified;
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, string userId)
        {
            var city = await GetByIdAsync(id);
            if (city == null)
                return false;

            // Check if city is being used by any schools
            var hasSchools = await _context.SchoolMasters.AnyAsync(s => s.CityId == id && !s.IsDeleted);
            if (hasSchools)
            {
                throw new InvalidOperationException("Cannot delete city as it is being used by one or more schools");
            }

            // Soft delete
            city.IsDeleted = true;
            city.ModifiedBy = Guid.Parse(userId);
            city.ModifiedDate = DateTime.UtcNow;

            _context.Entry(city).State = EntityState.Modified;
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.CityMasters
                .AnyAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<int> CountAsync(string searchTerm = "", Guid? stateId = null, Guid? countryId = null, bool? isActive = null)
        {
            IQueryable<CityMaster> query = _context.CityMasters
                .Where(c => !c.IsDeleted);

            if (stateId.HasValue)
            {
                query = query.Where(c => c.CityStateId == stateId.Value);
            }
            else if (countryId.HasValue)
            {
                query = query.Include(c => c.CityStateNavigation)
                           .Where(c => c.CityStateNavigation.CountryId == countryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.CityName.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                query = query.Where(c => c.IsActive == isActive.Value);
            }

            return await query.CountAsync();
        }
        #endregion
    }
}
