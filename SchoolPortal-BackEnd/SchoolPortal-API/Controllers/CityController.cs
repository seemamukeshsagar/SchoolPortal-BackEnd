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
using SchoolPortal_API.ViewModels.City;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Manages city-related operations
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;
        private readonly IMemoryCache _cache;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 100;
        private const string AllCitiesCacheKey = "all_cities";
        private const string CityCacheKeyPrefix = "city_";
        private const string CitiesByStateCacheKeyPrefix = "cities_state_";
        private const string CitiesByCountryCacheKeyPrefix = "cities_country_";

        public CityController(
            ICityService cityService,
            ILogger<CityController> logger,
            IMemoryCache cache)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets all cities with pagination and filtering
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedResponse<CityResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = "GetAllCities")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string searchTerm = "",
            [FromQuery] Guid? stateId = null,
            [FromQuery] Guid? countryId = null,
            [FromQuery] bool? isActive = null)
        {
            try
            {
                pageSize = Math.Min(Math.Max(1, pageSize), MaxPageSize);
                pageNumber = Math.Max(1, pageNumber);

                var cacheKey = $"{AllCitiesCacheKey}_{pageNumber}_{pageSize}_{searchTerm}_{stateId}_{countryId}_{isActive}";
                
                if (!_cache.TryGetValue(cacheKey, out PaginatedResponse<CityResponseDto> response))
                {
                    response = await _cityService.GetAllCitiesAsync(
                        pageNumber: pageNumber,
                        pageSize: pageSize,
                        searchTerm: searchTerm,
                        stateId: stateId,
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
                _logger.LogError(ex, "Error retrieving cities");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving cities" });
            }
        }

        /// <summary>
        /// Gets cities by state ID
        /// </summary>
        [HttpGet("by-state/{stateId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CityResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = "GetCitiesByState")]
        public async Task<IActionResult> GetByState(Guid stateId, [FromQuery] bool includeInactive = false)
        {
            try
            {
                var cacheKey = $"{CitiesByStateCacheKeyPrefix}{stateId}_{includeInactive}";
                
                if (!_cache.TryGetValue(cacheKey, out IEnumerable<CityResponseDto> cities))
                {
                    cities = await _cityService.GetCitiesByStateIdAsync(stateId, includeInactive);
                    
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                    _cache.Set(cacheKey, cities, cacheOptions);
                }

                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cities for state {StateId}", stateId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving cities" });
            }
        }

        /// <summary>
        /// Gets cities by country ID
        /// </summary>
        [HttpGet("by-country/{countryId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CityResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = "GetCitiesByCountry")]
        public async Task<IActionResult> GetByCountry(Guid countryId, [FromQuery] bool includeInactive = false)
        {
            try
            {
                var cacheKey = $"{CitiesByCountryCacheKeyPrefix}{countryId}_{includeInactive}";
                
                if (!_cache.TryGetValue(cacheKey, out IEnumerable<CityResponseDto> cities))
                {
                    cities = await _cityService.GetCitiesByCountryIdAsync(countryId, includeInactive);
                    
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(1));

                    _cache.Set(cacheKey, cities, cacheOptions);
                }

                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cities for country {CountryId}", countryId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving cities" });
            }
        }

        /// <summary>
        /// Gets a city by ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CityResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "GetCityById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var cacheKey = $"{CityCacheKeyPrefix}{id}";
                
                if (!_cache.TryGetValue(cacheKey, out CityResponseDto city))
                {
                    city = await _cityService.GetCityByIdAsync(id);
                    
                    if (city == null)
                    {
                        return NotFound(new { message = $"City with ID {id} not found" });
                    }

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(cacheKey, city, cacheOptions);
                }

                return Ok(city);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving city with ID {CityId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the city" });
            }
        }

        /// <summary>
        /// Creates a new city
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CityResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(OperationId = "CreateCity")]
        public async Task<IActionResult> Create([FromBody] CityDto cityDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid city data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var city = await _cityService.CreateCityAsync(cityDto, userId);

                if (city == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create city" });
                }

                // Invalidate caches
                _cache.Remove(AllCitiesCacheKey);
                _cache.Remove($"{CitiesByStateCacheKeyPrefix}{cityDto.StateId}_true");
                _cache.Remove($"{CitiesByStateCacheKeyPrefix}{cityDto.StateId}_false");
                _cache.Remove($"{CitiesByCountryCacheKeyPrefix}_true"); // Invalidate all country-based caches
                _cache.Remove($"{CitiesByCountryCacheKeyPrefix}_false");

                return CreatedAtAction(nameof(GetById), new { id = city.Id }, city);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while creating city");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while creating the city" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating city");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the city" });
            }
        }

        /// <summary>
        /// Updates an existing city
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CityResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "UpdateCity")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CityDto cityDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid city data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedCity = await _cityService.UpdateCityAsync(id, cityDto, userId);

                if (updatedCity == null)
                {
                    return NotFound(new { message = $"City with ID {id} not found" });
                }

                // Invalidate caches
                _cache.Remove(AllCitiesCacheKey);
                _cache.Remove($"{CityCacheKeyPrefix}{id}");
                _cache.Remove($"{CitiesByStateCacheKeyPrefix}{updatedCity.StateId}_true");
                _cache.Remove($"{CitiesByStateCacheKeyPrefix}{updatedCity.StateId}_false");
                _cache.Remove($"{CitiesByCountryCacheKeyPrefix}_true"); // Invalidate all country-based caches
                _cache.Remove($"{CitiesByCountryCacheKeyPrefix}_false");

                // If state was changed, invalidate the old state's cache too
                if (updatedCity.StateId != cityDto.StateId)
                {
                    _cache.Remove($"{CitiesByStateCacheKeyPrefix}{cityDto.StateId}_true");
                    _cache.Remove($"{CitiesByStateCacheKeyPrefix}{cityDto.StateId}_false");
                }

                return Ok(updatedCity);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating city with ID {CityId}", id);
                return StatusCode(StatusCodes.Status409Conflict, new { message = "The city was modified by another user. Please refresh and try again." });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while updating city with ID {CityId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while updating the city" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating city with ID {CityId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the city" });
            }
        }

        /// <summary>
        /// Deletes a city
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "DeleteCity")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                // Get the city first to get its StateId for cache invalidation
                var city = await _cityService.GetCityByIdAsync(id);
                if (city == null)
                {
                    return NotFound(new { message = $"City with ID {id} not found" });
                }

                var userId = GetCurrentUserId();
                var result = await _cityService.DeleteCityAsync(id, userId);

                if (!result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete city" });
                }

                // Invalidate caches
                _cache.Remove(AllCitiesCacheKey);
                _cache.Remove($"{CityCacheKeyPrefix}{id}");
                _cache.Remove($"{CitiesByStateCacheKeyPrefix}{city.StateId}_true");
                _cache.Remove($"{CitiesByStateCacheKeyPrefix}{city.StateId}_false");
                _cache.Remove($"{CitiesByCountryCacheKeyPrefix}_true"); // Invalidate all country-based caches
                _cache.Remove($"{CitiesByCountryCacheKeyPrefix}_false");

                return NoContent();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("cannot be deleted because it is in use"))
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while deleting city with ID {CityId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while deleting the city" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting city with ID {CityId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the city" });
            }
        }
    }
}
