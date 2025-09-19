using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ICompanyRepository : IRepository<CompanyMaster>
    {
        Task<IEnumerable<dynamic>> GetCountriesAsync();
        Task<IEnumerable<dynamic>> GetStatesByCountryIdAsync(Guid countryId);
        Task<IEnumerable<dynamic>> GetCitiesByStateIdAsync(Guid stateId);
        Task<CompanyMaster?> GetCompanyWithDetailsAsync(Guid id);
        
        // New methods for counting
        Task<int> CountByCityIdAsync(Guid cityId);
        Task<int> CountByJurisdictionCityIdAsync(Guid jurisdictionCityId);
        Task<int> CountByStateIdAsync(Guid stateId);
        Task<int> CountByJurisdictionStateIdAsync(Guid jurisdictionStateId);
        Task<int> CountByCountryIdAsync(Guid countryId);
        Task<int> CountByJurisdictionCountryIdAsync(Guid jurisdictionCountryId);
    }
}
