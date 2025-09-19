using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ISchoolRepository : IRepository<SchoolMaster>
    {
        // Read operations
        Task<SchoolMaster?> GetSchoolWithDetailsAsync(Guid id);
        Task<IEnumerable<SchoolMaster>> GetSchoolsByCompanyIdAsync(Guid companyId);
        
        // Create operation
        Task<SchoolMaster> CreateAsync(SchoolMaster school, Guid userId);
        
        // Update operation
        Task<bool> UpdateAsync(SchoolMaster school, Guid userId);
        
        // Delete operations
        Task<bool> DeleteAsync(Guid id, Guid userId);
        Task<bool> HardDeleteAsync(Guid id);
        
        // Utility operations
        Task<bool> ExistsAsync(Expression<Func<SchoolMaster, bool>> predicate);
        Task<int> CountAsync(Expression<Func<SchoolMaster, bool>> predicate = null);
        
        // Advanced query operations
        Task<IEnumerable<SchoolMaster>> FindAsync(
            Expression<Func<SchoolMaster, bool>> predicate = null,
            Func<IQueryable<SchoolMaster>, IOrderedQueryable<SchoolMaster>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null);
            
        // New methods for counting by city references
        Task<int> CountByCityIdAsync(Guid cityId);
        Task<int> CountByJurisdictionCityIdAsync(Guid jurisdictionCityId);
        Task<int> CountByBankCityIdAsync(Guid bankCityId);
        Task<int> CountByStateIdAsync(Guid stateId);
        Task<int> CountByJurisdictionStateIdAsync(Guid jurisdictionStateId);
        Task<int> CountByBankStateIdAsync(Guid bankStateId);
        Task<int> CountByCountryIdAsync(Guid countryId);
        Task<int> CountByJurisdictionCountryIdAsync(Guid jurisdictionCountryId);
        Task<int> CountByBankCountryIdAsync(Guid bankCountryId);
    }
}
