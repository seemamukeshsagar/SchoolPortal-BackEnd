using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface IStateRepository : IRepository<StateMaster>
    {
        Task<(IEnumerable<StateMaster> items, int totalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? countryId = null,
            bool? isActive = null);
            
        Task<IEnumerable<StateMaster>> GetByCountryIdAsync(Guid countryId, bool includeInactive = false);
        Task<StateMaster> GetByIdAsync(Guid id);
        Task<StateMaster> CreateAsync(StateMaster entity, string userId);
        Task<bool> UpdateAsync(StateMaster entity, string userId);
        Task<bool> DeleteAsync(Guid id, string userId);
        Task<bool> ExistsAsync(Guid id);
        Task<int> CountAsync(string searchTerm = "", Guid? countryId = null, bool? isActive = null);
    }
}
