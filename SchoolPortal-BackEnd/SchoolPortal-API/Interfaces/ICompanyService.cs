using SchoolPortal_API.ViewModels.Company;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyResponseDto>> GetAllCompaniesAsync();
        Task<CompanyResponseDto?> GetCompanyByIdAsync(Guid id);
        Task<CompanyResponseDto?> CreateCompanyAsync(CompanyDto companyDto, Guid userId);
        Task<CompanyResponseDto?> UpdateCompanyAsync(Guid id, CompanyDto companyDto, Guid userId);
        Task<bool> DeleteCompanyAsync(Guid id, Guid userId);
        Task<IEnumerable<dynamic>> GetCountriesAsync();
        Task<IEnumerable<dynamic>> GetStatesByCountryIdAsync(Guid countryId);
        Task<IEnumerable<dynamic>> GetCitiesByStateIdAsync(Guid stateId);
    }
}
