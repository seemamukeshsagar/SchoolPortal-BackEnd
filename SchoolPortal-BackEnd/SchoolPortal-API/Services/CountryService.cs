using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.Models;
using SchoolPortal_API.ViewModels.Country;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryService> _logger;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CountryService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResponse<CountryResponseDto>> GetAllCountriesAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            bool? isActive = null)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Countries.GetAllAsync(
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    searchTerm: searchTerm,
                    isActive: isActive);
                    
                return new PaginatedResponse<CountryResponseDto>
                {
                    Items = _mapper.Map<IEnumerable<CountryResponseDto>>(items),
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all countries");
                throw;
            }
        }

        public async Task<CountryResponseDto> GetCountryByIdAsync(Guid id)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetByIdAsync(id);
                if (country == null)
                {
                    _logger.LogWarning("Country with ID {CountryId} not found", id);
                    return null;
                }
                return _mapper.Map<CountryResponseDto>(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting country with ID {CountryId}", id);
                throw;
            }
        }

        public async Task<CountryResponseDto> CreateCountryAsync(CountryDto countryDto, string userId)
        {
            try
            {
                var country = _mapper.Map<CountryMaster>(countryDto);
                country.Id = Guid.NewGuid();
                country.CreatedBy = Guid.Parse(userId);
                country.CreatedDate = DateTime.UtcNow;
                country.IsDeleted = false;

                await _unitOfWork.Countries.AddAsync(country);
                await _unitOfWork.CompleteAsync();

                var createdCountry = await _unitOfWork.Countries.GetByIdAsync(country.Id);
                return _mapper.Map<CountryResponseDto>(createdCountry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating country");
                throw;
            }
        }

        public async Task<CountryResponseDto> UpdateCountryAsync(Guid id, CountryDto countryDto, string userId)
        {
            try
            {
                var existingCountry = await _unitOfWork.Countries.GetByIdAsync(id);
                if (existingCountry == null)
                {
                    _logger.LogWarning("Country with ID {CountryId} not found for update", id);
                    return null;
                }

                _mapper.Map(countryDto, existingCountry);
                existingCountry.ModifiedBy = Guid.Parse(userId);
                existingCountry.ModifiedDate = DateTime.UtcNow;

                _unitOfWork.Countries.Update(existingCountry);
                await _unitOfWork.CompleteAsync();

                var updatedCountry = await _unitOfWork.Countries.GetByIdAsync(id);
                return _mapper.Map<CountryResponseDto>(updatedCountry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating country with ID {CountryId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteCountryAsync(Guid id, string userId)
        {
            try
            {
                var country = await _unitOfWork.Countries.GetByIdAsync(id);
                if (country == null)
                {
                    _logger.LogWarning("Country with ID {CountryId} not found for deletion", id);
                    return false;
                }

                // Check if country is being used by any states
                var stateCount = await _unitOfWork.States.CountAsync(countryId: id);
                if (stateCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete country as it is being used by one or more states");
                }

                // Check if country is being used by any companies
                var companyCount = await _unitOfWork.Companies.CountByCountryIdAsync(id);
                if (companyCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete country as it is being used by one or more companies");
                }

                // Check if country is being used as a jurisdiction country by any companies
                var jurisdictionCompanyCount = await _unitOfWork.Companies.CountByJurisdictionCountryIdAsync(id);
                if (jurisdictionCompanyCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete country as it is being used as a jurisdiction country by one or more companies");
                }

                // Check if country is being used by any schools
                var schoolCount = await _unitOfWork.Schools.CountByCountryIdAsync(id);
                if (schoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete country as it is being used by one or more schools");
                }

                // Check if country is being used as a jurisdiction country by any schools
                var jurisdictionSchoolCount = await _unitOfWork.Schools.CountByJurisdictionCountryIdAsync(id);
                if (jurisdictionSchoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete country as it is being used as a jurisdiction country by one or more schools");
                }

                // Check if country is being used as a bank country by any schools
                var bankSchoolCount = await _unitOfWork.Schools.CountByBankCountryIdAsync(id);
                if (bankSchoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete country as it is being used as a bank country by one or more schools");
                }

                // Soft delete
                country.ModifiedBy = Guid.Parse(userId);
                country.ModifiedDate = DateTime.UtcNow;

                _unitOfWork.Countries.Update(country);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting country with ID {CountryId}", id);
                throw;
            }
        }

        public async Task<int> GetCountriesCountAsync(string searchTerm = "", bool? isActive = null)
        {
            try
            {
                return await _unitOfWork.Countries.CountAsync(searchTerm, isActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting countries count");
                throw;
            }
        }
    }
}
