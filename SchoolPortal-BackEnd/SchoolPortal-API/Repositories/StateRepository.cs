using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.ViewModels.State;
using SchoolPortal_API.Interfaces;

namespace SchoolPortal_API.Repositories
{
    public class StateRepository : IRepository<StateMaster>, IStateRepository
    {
        private readonly SchoolNewPortalContext _context;

        public StateRepository(SchoolNewPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region IRepository Implementation
        
        public async Task<StateMaster?> GetByIdAsync(Guid id)
        {
            return await _context.StateMasters
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<IEnumerable<StateMaster>> GetAllAsync()
        {
            return await _context.StateMasters
                .Include(s => s.Country)
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.StateName)
                .ToListAsync();
        }

        public async Task<IEnumerable<StateMaster>> FindAsync(Expression<Func<StateMaster, bool>> predicate)
        {
            return await _context.StateMasters
                .Include(s => s.Country)
                .Where(s => !s.IsDeleted)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task AddAsync(StateMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.StateMasters.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<StateMaster> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            await _context.StateMasters.AddRangeAsync(entities);
        }

        public void Update(StateMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(StateMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RemoveRange(IEnumerable<StateMaster> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<StateMaster, bool>> predicate)
        {
            return await _context.StateMasters
                .Where(s => !s.IsDeleted)
                .AnyAsync(predicate);
        }

        #endregion

        #region IStateRepository Implementation

        public async Task<(IEnumerable<StateMaster> items, int totalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? countryId = null,
            bool? isActive = null)
        {
            IQueryable<StateMaster> query = _context.StateMasters
                .Include(s => s.Country)
                .Where(s => !s.IsDeleted);

            if (countryId.HasValue)
            {
                query = query.Where(s => s.CountryId == countryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.StateName.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(s => s.StateName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<IEnumerable<StateMaster>> GetByCountryIdAsync(Guid countryId, bool includeInactive = false)
        {
            IQueryable<StateMaster> query = _context.StateMasters
                .Include(s => s.Country)
                .Where(s => s.CountryId == countryId && !s.IsDeleted);

            if (!includeInactive)
            {
                query = query.Where(s => s.IsActive);
            }

            return await query
                .OrderBy(s => s.StateName)
                .ToListAsync();
        }

        async Task<StateMaster> IStateRepository.GetByIdAsync(Guid id)
        {
            var state = await GetByIdAsync(id);
            if (state == null)
                throw new KeyNotFoundException($"State with ID {id} not found");
            return state;
        }

        public async Task<StateMaster> CreateAsync(StateMaster entity, string userId)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(userId);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;

            await _context.StateMasters.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(StateMaster entity, string userId)
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
            var state = await GetByIdAsync(id);
            if (state == null)
                return false;

            // Check if state is being used by any cities
            var hasCities = await _context.CityMasters.AnyAsync(c => c.CityStateId == id && !c.IsDeleted);
            if (hasCities)
            {
                throw new InvalidOperationException("Cannot delete state as it is being used by one or more cities");
            }

            // Check if state is being used by any schools
            var hasSchools = await _context.SchoolMasters.AnyAsync(s => s.StateId == id && !s.IsDeleted);
            if (hasSchools)
            {
                throw new InvalidOperationException("Cannot delete state as it is being used by one or more schools");
            }

            // Soft delete
            state.IsDeleted = true;
            state.ModifiedBy = Guid.Parse(userId);
            state.ModifiedDate = DateTime.UtcNow;

            _context.Entry(state).State = EntityState.Modified;
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.StateMasters
                .AnyAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<int> CountAsync(string searchTerm = "", Guid? countryId = null, bool? isActive = null)
        {
            IQueryable<StateMaster> query = _context.StateMasters
                .Where(s => !s.IsDeleted);

            if (countryId.HasValue)
            {
                query = query.Where(s => s.CountryId == countryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.StateName.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }

            return await query.CountAsync();
        }
        #endregion
    }
}
