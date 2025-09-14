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
    }
}
