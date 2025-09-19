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
using SchoolPortal_API.ViewModels.Country;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Manages country-related operations
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;
        private readonly IMemoryCache _cache;
        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 100;
        private const string AllCountriesCacheKey = "all_countries";
        private const string CountryCacheKeyPrefix = "country_";

        public CountryController(
            ICountryService countryService,
            ILogger<CountryController> logger,
            IMemoryCache cache)
        {
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets all countries with pagination and filtering
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PaginatedResponse<CountryResponseDto>), StatusCodes.Status200OK)]
        [SwaggerOperation(OperationId = "GetAllCountries")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool? isActive = null)
        {
            try
            {
                pageSize = Math.Min(Math.Max(1, pageSize), MaxPageSize);
                pageNumber = Math.Max(1, pageNumber);

                var cacheKey = $"{AllCountriesCacheKey}_{pageNumber}_{pageSize}_{searchTerm}_{isActive}";
                
                if (!_cache.TryGetValue(cacheKey, out PaginatedResponse<CountryResponseDto> response))
                {
                    response = await _countryService.GetAllCountriesAsync(pageNumber, pageSize, searchTerm, isActive);
                    
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
                _logger.LogError(ex, "Error occurred while getting paginated countries");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving countries");
            }
        }

        /// <summary>
        /// Gets a country by ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CountryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "GetCountryById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var cacheKey = $"{CountryCacheKeyPrefix}{id}";
                
                if (!_cache.TryGetValue(cacheKey, out CountryResponseDto country))
                {
                    country = await _countryService.GetCountryByIdAsync(id);
                    
                    if (country == null)
                    {
                        return NotFound(new { message = $"Country with ID {id} not found" });
                    }

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(cacheKey, country, cacheOptions);
                }

                return Ok(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving country with ID {CountryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the country" });
            }
        }

        /// <summary>
        /// Creates a new country
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CountryResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(OperationId = "CreateCountry")]
        public async Task<IActionResult> Create([FromBody] CountryDto countryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid country data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var country = await _countryService.CreateCountryAsync(countryDto, userId);

                if (country == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create country" });
                }

                // Invalidate the all countries cache
                _cache.Remove(AllCountriesCacheKey);

                return CreatedAtAction(nameof(GetById), new { id = country.Id }, country);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while creating country");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while creating the country" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating country");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the country" });
            }
        }

        /// <summary>
        /// Updates an existing country
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CountryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "UpdateCountry")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CountryDto countryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid country data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedCountry = await _countryService.UpdateCountryAsync(id, countryDto, userId);

                if (updatedCountry == null)
                {
                    return NotFound(new { message = $"Country with ID {id} not found" });
                }

                // Invalidate caches
                _cache.Remove(AllCountriesCacheKey);
                _cache.Remove($"{CountryCacheKeyPrefix}{id}");

                return Ok(updatedCountry);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating country with ID {CountryId}", id);
                return StatusCode(StatusCodes.Status409Conflict, new { message = "The country was modified by another user. Please refresh and try again." });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while updating country with ID {CountryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while updating the country" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating country with ID {CountryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the country" });
            }
        }

        /// <summary>
        /// Deletes a country
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(OperationId = "DeleteCountry")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _countryService.DeleteCountryAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Country with ID {id} not found" });
                }

                // Invalidate caches
                _cache.Remove(AllCountriesCacheKey);
                _cache.Remove($"{CountryCacheKeyPrefix}{id}");

                return NoContent();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("cannot be deleted because it is in use"))
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while deleting country with ID {CountryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while deleting the country" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting country with ID {CountryId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the country" });
            }
        }
    }
}
