using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ICountryRepository : IRepository<CountryMaster>
    {
        Task<(IEnumerable<CountryMaster> items, int totalCount)> GetAllAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            bool? isActive = null);
            
        Task<CountryMaster> GetByIdAsync(Guid id);
        Task<CountryMaster> CreateAsync(CountryMaster entity, string userId);
        Task<bool> UpdateAsync(CountryMaster entity, string userId);
        Task<bool> DeleteAsync(Guid id, string userId);
        Task<bool> ExistsAsync(Guid id);
        Task<int> CountAsync(string searchTerm = "", bool? isActive = null);
    }
}
