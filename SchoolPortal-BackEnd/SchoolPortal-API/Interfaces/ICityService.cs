using SchoolPortal_API.ViewModels.City;
using SchoolPortal_API.Models; // For PaginatedResponse
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ICityService
    {
        Task<PaginatedResponse<CityResponseDto>> GetAllCitiesAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? stateId = null,
            Guid? countryId = null,
            bool? isActive = null);
        Task<CityResponseDto> GetCityByIdAsync(Guid id);
        Task<IEnumerable<CityResponseDto>> GetCitiesByStateIdAsync(Guid stateId, bool includeInactive = false);
        Task<IEnumerable<CityResponseDto>> GetCitiesByCountryIdAsync(Guid countryId, bool includeInactive = false);
        Task<CityResponseDto> CreateCityAsync(CityDto cityDto, string userId);
        Task<CityResponseDto> UpdateCityAsync(Guid id, CityDto cityDto, string userId);
        Task<bool> DeleteCityAsync(Guid id, string userId);
        Task<int> GetCitiesCountAsync(string searchTerm = "", Guid? stateId = null, Guid? countryId = null, bool? isActive = null);
    }
}
