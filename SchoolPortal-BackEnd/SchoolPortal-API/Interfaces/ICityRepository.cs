using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ICityRepository : IRepository<CityMaster>
    {
        Task<(IEnumerable<CityMaster> items, int totalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? stateId = null,
            Guid? countryId = null,
            bool? isActive = null);
            
        Task<IEnumerable<CityMaster>> GetByStateIdAsync(Guid stateId, bool includeInactive = false);
        Task<IEnumerable<CityMaster>> GetByCountryIdAsync(Guid countryId, bool includeInactive = false);
        Task<CityMaster> GetByIdAsync(Guid id);
        Task<CityMaster> CreateAsync(CityMaster entity, string userId);
        Task<bool> UpdateAsync(CityMaster entity, string userId);
        Task<bool> DeleteAsync(Guid id, string userId);
        Task<bool> ExistsAsync(Guid id);
        Task<int> CountAsync(string searchTerm = "", Guid? stateId = null, Guid? countryId = null, bool? isActive = null);
    }
}
