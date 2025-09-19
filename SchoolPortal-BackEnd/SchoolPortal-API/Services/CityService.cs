using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.Models;
using SchoolPortal_API.ViewModels.City;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Models; // For PaginatedResponse
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CityService> _logger;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CityService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResponse<CityResponseDto>> GetAllCitiesAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? stateId = null,
            Guid? countryId = null,
            bool? isActive = null)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Cities.GetAllAsync(
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    searchTerm: searchTerm,
                    stateId: stateId,
                    countryId: countryId,
                    isActive: isActive);
                    
                return new PaginatedResponse<CityResponseDto>
                {
                    Items = _mapper.Map<IEnumerable<CityResponseDto>>(items),
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting paginated cities");
                throw;
            }
        }

        public async Task<CityResponseDto> GetCityByIdAsync(Guid id)
        {
            try
            {
                var city = await _unitOfWork.Cities.GetByIdAsync(id);
                if (city == null)
                {
                    _logger.LogWarning("City with ID {CityId} not found", id);
                    return null;
                }
                
                var response = _mapper.Map<CityResponseDto>(city);
                if (city.CityStateNavigation != null)
                {
                    response.StateName = city.CityStateNavigation.StateName;
                    if (city.CityStateNavigation.Country != null)
                    {
                        response.CountryName = city.CityStateNavigation.Country.CountryName;
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting city with ID {CityId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<CityResponseDto>> GetCitiesByStateIdAsync(Guid stateId, bool includeInactive = false)
        {
            try
            {
                var cities = await _unitOfWork.Cities.GetByStateIdAsync(stateId, includeInactive);
                return _mapper.Map<IEnumerable<CityResponseDto>>(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities for state {StateId}", stateId);
                throw;
            }
        }

        public async Task<IEnumerable<CityResponseDto>> GetCitiesByCountryIdAsync(Guid countryId, bool includeInactive = false)
        {
            try
            {
                var cities = await _unitOfWork.Cities.GetByCountryIdAsync(countryId, includeInactive);
                return _mapper.Map<IEnumerable<CityResponseDto>>(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities for country {CountryId}", countryId);
                throw;
            }
        }

        public async Task<CityResponseDto> CreateCityAsync(CityDto cityDto, string userId)
        {
            try
            {
                // Verify state exists
                var state = await _unitOfWork.States.GetByIdAsync(cityDto.StateId);
                if (state == null)
                {
                    throw new KeyNotFoundException($"State with ID {cityDto.StateId} not found");
                }

                var city = _mapper.Map<CityMaster>(cityDto);
                city.Id = Guid.NewGuid();
                city.CityStateId = cityDto.StateId;
                city.CreatedBy = Guid.Parse(userId);
                city.CreatedDate = DateTime.UtcNow;
                city.IsDeleted = false;

                await _unitOfWork.Cities.AddAsync(city);
                await _unitOfWork.CompleteAsync();

                var createdCity = await _unitOfWork.Cities.GetByIdAsync(city.Id);
                var response = _mapper.Map<CityResponseDto>(createdCity);
                response.StateName = state.StateName;
                if (state.Country != null)
                {
                    response.CountryName = state.Country.CountryName;
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating city");
                throw;
            }
        }

        public async Task<CityResponseDto> UpdateCityAsync(Guid id, CityDto cityDto, string userId)
        {
            try
            {
                var existingCity = await _unitOfWork.Cities.GetByIdAsync(id);
                if (existingCity == null)
                {
                    _logger.LogWarning("City with ID {CityId} not found for update", id);
                    return null;
                }

                // If state is being changed, verify the new state exists
                if (existingCity.CityStateId != cityDto.StateId)
                {
                    var state = await _unitOfWork.States.GetByIdAsync(cityDto.StateId);
                    if (state == null)
                    {
                        throw new KeyNotFoundException($"State with ID {cityDto.StateId} not found");
                    }
                }

                _mapper.Map(cityDto, existingCity);
                existingCity.CityStateId = cityDto.StateId;
                existingCity.ModifiedBy = Guid.Parse(userId);
                existingCity.ModifiedDate = DateTime.UtcNow;

                _unitOfWork.Cities.Update(existingCity);
                await _unitOfWork.CompleteAsync();

                var updatedCity = await _unitOfWork.Cities.GetByIdAsync(id);
                var response = _mapper.Map<CityResponseDto>(updatedCity);
                if (updatedCity.CityStateNavigation != null)
                {
                    response.StateName = updatedCity.CityStateNavigation.StateName;
                    if (updatedCity.CityStateNavigation.Country != null)
                    {
                        response.CountryName = updatedCity.CityStateNavigation.Country.CountryName;
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating city with ID {CityId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteCityAsync(Guid id, string userId)
        {
            try
            {
                var city = await _unitOfWork.Cities.GetByIdAsync(id);
                if (city == null)
                {
                    _logger.LogWarning("City with ID {CityId} not found for deletion", id);
                    return false;
                }

                // Check if city is being used by any companies
                var companyCount = await _unitOfWork.Companies.CountByCityIdAsync(id);
                if (companyCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete city as it is being used by one or more companies");
                }

                // Check if city is being used as a jurisdiction area by any companies
                var jurisdictionCompanyCount = await _unitOfWork.Companies.CountByJurisdictionCityIdAsync(id);
                if (jurisdictionCompanyCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete city as it is being used as a jurisdiction area by one or more companies");
                }

                // Check if city is being used by any schools
                var schoolCount = await _unitOfWork.Schools.CountByCityIdAsync(id);
                if (schoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete city as it is being used by one or more schools");
                }

                // Check if city is being used as a jurisdiction city by any schools
                var jurisdictionSchoolCount = await _unitOfWork.Schools.CountByJurisdictionCityIdAsync(id);
                if (jurisdictionSchoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete city as it is being used as a jurisdiction city by one or more schools");
                }

                // Check if city is being used as a bank city by any schools
                var bankSchoolCount = await _unitOfWork.Schools.CountByBankCityIdAsync(id);
                if (bankSchoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete city as it is being used as a bank city by one or more schools");
                }

                // Soft delete
                city.IsDeleted = true;
                city.ModifiedBy = Guid.Parse(userId);
                city.ModifiedDate = DateTime.UtcNow;

                _unitOfWork.Cities.Update(city);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting city with ID {CityId}", id);
                throw;
            }
        }

        public async Task<int> GetCitiesCountAsync(string searchTerm = "", Guid? stateId = null, Guid? countryId = null, bool? isActive = null)
        {
            try
            {
                return await _unitOfWork.Cities.CountAsync(searchTerm, stateId, countryId, isActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting cities count");
                throw;
            }
        }
    }
}
