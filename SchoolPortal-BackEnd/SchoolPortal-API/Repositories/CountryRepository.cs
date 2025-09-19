using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Country;

namespace SchoolPortal_API.Repositories
{
    public class CountryRepository : IRepository<CountryMaster>, ICountryRepository
    {
        private readonly SchoolPortalContext _context;

        public CountryRepository(SchoolPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region IRepository Implementation
        
        public async Task<CountryMaster?> GetByIdAsync(Guid id)
        {
            return await _context.CountryMasters
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<CountryMaster>> GetAllAsync()
        {
            return await _context.CountryMasters
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.CountryName)
                .ToListAsync();
        }

        public async Task<IEnumerable<CountryMaster>> FindAsync(Expression<Func<CountryMaster, bool>> predicate)
        {
            return await _context.CountryMasters
                .Where(c => !c.IsDeleted)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task AddAsync(CountryMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _context.CountryMasters.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<CountryMaster> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            await _context.CountryMasters.AddRangeAsync(entities);
        }

        public void Update(CountryMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(CountryMaster entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RemoveRange(IEnumerable<CountryMaster> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<CountryMaster, bool>> predicate)
        {
            return await _context.CountryMasters
                .Where(c => !c.IsDeleted)
                .AnyAsync(predicate);
        }

        #endregion

        #region ICountryRepository Implementation

        public async Task<(IEnumerable<CountryMaster> items, int totalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            bool? isActive = null)
        {
            IQueryable<CountryMaster> query = _context.CountryMasters
                .Where(c => !c.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.CountryName.Contains(searchTerm));
            }

            if (isActive.HasValue)
            {
                query = query.Where(c => c.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.CountryName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        async Task<CountryMaster> ICountryRepository.GetByIdAsync(Guid id)
        {
            var country = await GetByIdAsync(id);
            if (country == null)
                throw new KeyNotFoundException($"Country with ID {id} not found");
            return country;
        }

        public async Task<CountryMaster> CreateAsync(CountryMaster entity, string userId)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(userId);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;

            await _context.CountryMasters.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(CountryMaster entity, string userId)
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
            var country = await GetByIdAsync(id);
            if (country == null)
                return false;

            // Soft delete
            country.IsDeleted = true;
            country.ModifiedBy = Guid.Parse(userId);
            country.ModifiedDate = DateTime.UtcNow;

            _context.Entry(country).State = EntityState.Modified;
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.CountryMasters
                .AnyAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<int> CountAsync(string searchTerm = "", bool? isActive = null)
        {
            IQueryable<CountryMaster> query = _context.CountryMasters
                .Where(c => !c.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.CountryName.Contains(searchTerm));
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
