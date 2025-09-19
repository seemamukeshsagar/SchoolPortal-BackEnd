using SchoolPortal_API.ViewModels.Country;
using SchoolPortal_API.Models; // For PaginatedResponse
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ICountryService
    {
        Task<PaginatedResponse<CountryResponseDto>> GetAllCountriesAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            bool? isActive = null);
        Task<CountryResponseDto> GetCountryByIdAsync(Guid id);
        Task<CountryResponseDto> CreateCountryAsync(CountryDto countryDto, string userId);
        Task<CountryResponseDto> UpdateCountryAsync(Guid id, CountryDto countryDto, string userId);
        Task<bool> DeleteCountryAsync(Guid id, string userId);
        Task<int> GetCountriesCountAsync(string searchTerm = "", bool? isActive = null);
    }
}
