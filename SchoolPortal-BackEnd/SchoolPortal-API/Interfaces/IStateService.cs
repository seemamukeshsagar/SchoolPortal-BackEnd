using SchoolPortal_API.ViewModels.State;
using SchoolPortal_API.Models; // For PaginatedResponse
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface IStateService
    {
        Task<PaginatedResponse<StateResponseDto>> GetAllStatesAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string searchTerm = "",
            Guid? countryId = null,
            bool? isActive = null);
        Task<StateResponseDto> GetStateByIdAsync(Guid id);
        Task<IEnumerable<StateResponseDto>> GetStatesByCountryIdAsync(Guid countryId, bool includeInactive = false);
        Task<StateResponseDto> CreateStateAsync(StateDto stateDto, string userId);
        Task<StateResponseDto> UpdateStateAsync(Guid id, StateDto stateDto, string userId);
        Task<bool> DeleteStateAsync(Guid id, string userId);
        Task<int> GetStatesCountAsync(string searchTerm = "", Guid? countryId = null, bool? isActive = null);
    }
}
