using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.Models;
using SchoolPortal_API.ViewModels.State;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Manages state-related operations
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _stateService;
        private readonly ILogger<StateController> _logger;
        private readonly IMemoryCache _cache;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 100;
        private const string AllStatesCacheKey = "all_states";
        private const string StateCacheKeyPrefix = "state_";
        private const string StatesByCountryCacheKeyPrefix = "states_country_";

        public StateController(
            IStateService stateService,
            ILogger<StateController> logger,
            IMemoryCache cache)
        {
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets all states with pagination and filtering
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedResponse<StateResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = "GetAllStates")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string searchTerm = "",
            [FromQuery] Guid? countryId = null,
            [FromQuery] bool? isActive = null)
        {
            try
            {
                pageSize = Math.Min(Math.Max(1, pageSize), MaxPageSize);
                pageNumber = Math.Max(1, pageNumber);

                var cacheKey = $"{AllStatesCacheKey}_{pageNumber}_{pageSize}_{searchTerm}_{countryId}_{isActive}";
                
                if (!_cache.TryGetValue(cacheKey, out PaginatedResponse<StateResponseDto> response))
                {
                    response = await _stateService.GetAllStatesAsync(
                        pageNumber: pageNumber,
                        pageSize: pageSize,
                        searchTerm: searchTerm,
                        countryId: countryId,
                        isActive: isActive);
                    
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                    _cache.Set(cacheKey, response, cacheOptions);
                }
                
                // Add pagination headers for API consumers
                Response.Headers.Add("X-Pagination", 
                    System.Text.Json.JsonSerializer.Serialize(new 
                    { 
                        response.TotalCount, 
                        response.PageSize, 
                        response.PageNumber, 
                        response.TotalPages,
                        HasPrevious = response.PageNumber > 1,
                        HasNext = response.PageNumber < response.TotalPages
                    }));

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving states");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving states" });
            }
        }

        /// <summary>
        /// Gets states by country ID
        /// </summary>
        [HttpGet("by-country/{countryId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<StateResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = "GetStatesByCountry")]
        public async Task<IActionResult> GetByCountry(Guid countryId, [FromQuery] bool includeInactive = false)
        {
            try
            {
                var cacheKey = $"{StatesByCountryCacheKeyPrefix}{countryId}_{includeInactive}";
                
                if (!_cache.TryGetValue(cacheKey, out IEnumerable<StateResponseDto> states))
                {
                    states = await _stateService.GetStatesByCountryIdAsync(countryId, includeInactive);
                    
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                    _cache.Set(cacheKey, states, cacheOptions);
                }

                return Ok(states);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving states for country {CountryId}", countryId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving states" });
            }
        }

        /// <summary>
        /// Gets a state by ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(StateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "GetStateById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var cacheKey = $"{StateCacheKeyPrefix}{id}";
                
                if (!_cache.TryGetValue(cacheKey, out StateResponseDto state))
                {
                    state = await _stateService.GetStateByIdAsync(id);
                    
                    if (state == null)
                    {
                        return NotFound(new { message = $"State with ID {id} not found" });
                    }

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(cacheKey, state, cacheOptions);
                }

                return Ok(state);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving state with ID {StateId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the state" });
            }
        }

        /// <summary>
        /// Creates a new state
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(StateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(OperationId = "CreateState")]
        public async Task<IActionResult> Create([FromBody] StateDto stateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid state data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var state = await _stateService.CreateStateAsync(stateDto, userId);

                if (state == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create state" });
                }

                // Invalidate caches
                _cache.Remove(AllStatesCacheKey);
                _cache.Remove($"{StatesByCountryCacheKeyPrefix}{stateDto.CountryId}_true");
                _cache.Remove($"{StatesByCountryCacheKeyPrefix}{stateDto.CountryId}_false");

                return CreatedAtAction(nameof(GetById), new { id = state.Id }, state);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while creating state");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while creating the state" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating state");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the state" });
            }
        }

        /// <summary>
        /// Updates an existing state
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(StateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "UpdateState")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StateDto stateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid state data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedState = await _stateService.UpdateStateAsync(id, stateDto, userId);

                if (updatedState == null)
                {
                    return NotFound(new { message = $"State with ID {id} not found" });
                }

                // Invalidate caches
                _cache.Remove(AllStatesCacheKey);
                _cache.Remove($"{StateCacheKeyPrefix}{id}");
                _cache.Remove($"{StatesByCountryCacheKeyPrefix}{updatedState.CountryId}_true");
                _cache.Remove($"{StatesByCountryCacheKeyPrefix}{updatedState.CountryId}_false");

                return Ok(updatedState);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating state with ID {StateId}", id);
                return StatusCode(StatusCodes.Status409Conflict, new { message = "The state was modified by another user. Please refresh and try again." });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while updating state with ID {StateId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while updating the state" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating state with ID {StateId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the state" });
            }
        }

        /// <summary>
        /// Deletes a state
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "DeleteState")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                // Get the state first to get its CountryId for cache invalidation
                var state = await _stateService.GetStateByIdAsync(id);
                if (state == null)
                {
                    return NotFound(new { message = $"State with ID {id} not found" });
                }

                var userId = GetCurrentUserId();
                var result = await _stateService.DeleteStateAsync(id, userId);

                if (!result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete state" });
                }

                // Invalidate caches
                _cache.Remove(AllStatesCacheKey);
                _cache.Remove($"{StateCacheKeyPrefix}{id}");
                _cache.Remove($"{StatesByCountryCacheKeyPrefix}{state.CountryId}_true");
                _cache.Remove($"{StatesByCountryCacheKeyPrefix}{state.CountryId}_false");

                return NoContent();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("cannot be deleted because it is in use"))
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while deleting state with ID {StateId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while deleting the state" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting state with ID {StateId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the state" });
            }
        }
    }
}
