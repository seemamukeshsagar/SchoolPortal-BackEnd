using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.Models;
using SchoolPortal_API.ViewModels.State;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StateService> _logger;

        public StateService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StateService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResponse<StateResponseDto>> GetAllStatesAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? countryId = null,
            bool? isActive = null)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.States.GetAllAsync(
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    searchTerm: searchTerm,
                    countryId: countryId,
                    isActive: isActive);
                    
                return new PaginatedResponse<StateResponseDto>
                {
                    Items = _mapper.Map<IEnumerable<StateResponseDto>>(items),
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting paginated states");
                throw;
            }
        }

        public async Task<StateResponseDto> GetStateByIdAsync(Guid id)
        {
            try
            {
                var state = await _unitOfWork.States.GetByIdAsync(id);
                if (state == null)
                {
                    _logger.LogWarning("State with ID {StateId} not found", id);
                    return null;
                }
                
                var response = _mapper.Map<StateResponseDto>(state);
                if (state.Country != null)
                {
                    response.CountryName = state.Country.CountryName;
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting state with ID {StateId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<StateResponseDto>> GetStatesByCountryIdAsync(Guid countryId, bool includeInactive = false)
        {
            try
            {
                var states = await _unitOfWork.States.GetByCountryIdAsync(countryId, includeInactive);
                return _mapper.Map<IEnumerable<StateResponseDto>>(states);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting states for country {CountryId}", countryId);
                throw;
            }
        }

        public async Task<StateResponseDto> CreateStateAsync(StateDto stateDto, string userId)
        {
            try
            {
                // Verify country exists
                var country = await _unitOfWork.Countries.GetByIdAsync(stateDto.CountryId);
                if (country == null)
                {
                    throw new KeyNotFoundException($"Country with ID {stateDto.CountryId} not found");
                }

                var state = _mapper.Map<StateMaster>(stateDto);
                state.Id = Guid.NewGuid();
                state.CreatedBy = Guid.Parse(userId);
                state.CreatedDate = DateTime.UtcNow;
                state.IsDeleted = false;

                await _unitOfWork.States.AddAsync(state);
                await _unitOfWork.CompleteAsync();

                var createdState = await _unitOfWork.States.GetByIdAsync(state.Id);
                var response = _mapper.Map<StateResponseDto>(createdState);
                response.CountryName = country.CountryName;
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating state");
                throw;
            }
        }

        public async Task<StateResponseDto> UpdateStateAsync(Guid id, StateDto stateDto, string userId)
        {
            try
            {
                var existingState = await _unitOfWork.States.GetByIdAsync(id);
                if (existingState == null)
                {
                    _logger.LogWarning("State with ID {StateId} not found for update", id);
                    return null;
                }

                // If country is being changed, verify the new country exists
                if (existingState.CountryId != stateDto.CountryId)
                {
                    var country = await _unitOfWork.Countries.GetByIdAsync(stateDto.CountryId);
                    if (country == null)
                    {
                        throw new KeyNotFoundException($"Country with ID {stateDto.CountryId} not found");
                    }
                }

                _mapper.Map(stateDto, existingState);
                existingState.ModifiedBy = Guid.Parse(userId);
                existingState.ModifiedDate = DateTime.UtcNow;

                _unitOfWork.States.Update(existingState);
                await _unitOfWork.CompleteAsync();

                var updatedState = await _unitOfWork.States.GetByIdAsync(id);
                var response = _mapper.Map<StateResponseDto>(updatedState);
                if (updatedState.Country != null)
                {
                    response.CountryName = updatedState.Country.CountryName;
                }
                
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating state with ID {StateId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteStateAsync(Guid id, string userId)
        {
            try
            {
                var state = await _unitOfWork.States.GetByIdAsync(id);
                if (state == null)
                {
                    _logger.LogWarning("State with ID {StateId} not found for deletion", id);
                    return false;
                }

                // Check if state is being used by any cities
                var cityCount = await _unitOfWork.Cities.CountAsync(stateId: id);
                if (cityCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete state as it is being used by one or more cities");
                }

                // Check if state is being used by any companies
                var companyCount = await _unitOfWork.Companies.CountByStateIdAsync(id);
                if (companyCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete state as it is being used by one or more companies");
                }

                // Check if state is being used as a jurisdiction state by any companies
                var jurisdictionCompanyCount = await _unitOfWork.Companies.CountByJurisdictionStateIdAsync(id);
                if (jurisdictionCompanyCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete state as it is being used as a jurisdiction state by one or more companies");
                }

                // Check if state is being used by any schools
                var schoolCount = await _unitOfWork.Schools.CountByStateIdAsync(id);
                if (schoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete state as it is being used by one or more schools");
                }

                // Check if state is being used as a jurisdiction state by any schools
                var jurisdictionSchoolCount = await _unitOfWork.Schools.CountByJurisdictionStateIdAsync(id);
                if (jurisdictionSchoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete state as it is being used as a jurisdiction state by one or more schools");
                }

                // Check if state is being used as a bank state by any schools
                var bankSchoolCount = await _unitOfWork.Schools.CountByBankStateIdAsync(id);
                if (bankSchoolCount > 0)
                {
                    throw new InvalidOperationException("Cannot delete state as it is being used as a bank state by one or more schools");
                }

                // Soft delete
                state.IsDeleted = true;
                state.ModifiedBy = Guid.Parse(userId);
                state.ModifiedDate = DateTime.UtcNow;

                _unitOfWork.States.Update(state);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting state with ID {StateId}", id);
                throw;
            }
        }

        public async Task<int> GetStatesCountAsync(string searchTerm = "", Guid? countryId = null, bool? isActive = null)
        {
            try
            {
                return await _unitOfWork.States.CountAsync(searchTerm, countryId, isActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting states count");
                throw;
            }
        }
    }
}
