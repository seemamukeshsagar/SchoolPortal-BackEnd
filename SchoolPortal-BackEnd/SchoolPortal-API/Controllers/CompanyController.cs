using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Company;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides company management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;
        private readonly IMemoryCache _cache;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger, IMemoryCache cache)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        private Guid GetCurrentUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedAccessException("Invalid user ID in token");
            }
            return parsedUserId;
        }

        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>List of all companies</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all companies",
            Description = "Retrieves a list of all companies",
            OperationId = "GetAllCompanies"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CompanyResponseDto>>> GetAllCompanies()
        {
            try
            {
                var cacheKey = "all_companies";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<CompanyResponseDto> companies)
                {
                    companies = await _companyService.GetAllCompaniesAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, companies, cacheEntryOptions);
                }
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving companies");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets a company by ID
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns>Company details</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get company by ID",
            Description = "Retrieves a company by its unique identifier",
            OperationId = "GetCompanyById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CompanyResponseDto>> GetCompanyById(Guid id)
        {
            try
            {
                var cacheKey = $"company_{id}";
                if (!_cache.TryGetValue(cacheKey, out CompanyResponseDto? company))
                {
                    company = await _companyService.GetCompanyByIdAsync(id);
                    if (company != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, company, cacheEntryOptions);
                    }
                }
                if (company == null)
                {
                    return NotFound(new { message = $"Company with ID {id} not found" });
                }
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving company with ID {CompanyId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Creates a new company
        /// </summary>
        /// <param name="companyDto">Company data</param>
        /// <returns>Created company details</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Create a new company",
            Description = "Creates a new company with the provided data",
            OperationId = "CreateCompany"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompanyResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CompanyResponseDto>> CreateCompany([FromBody] CompanyDto companyDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var createdCompany = await _companyService.CreateCompanyAsync(companyDto, userId);

                if (createdCompany == null)
                {
                    _logger.LogError("Failed to create company");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create company" });
                }

                // Invalidate related caches
                _cache.Remove("all_companies");
                if (createdCompany.Id != Guid.Empty)
                {
                    _cache.Remove($"company_{createdCompany.Id}");
                }

                return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompany.Id }, createdCompany);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a company");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Updates an existing company
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <param name="companyDto">Updated company data</param>
        /// <returns>Updated company details</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Update a company",
            Description = "Updates an existing company with the provided data",
            OperationId = "UpdateCompany"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<CompanyResponseDto>> UpdateCompany(Guid id, [FromBody] CompanyDto companyDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedCompany = await _companyService.UpdateCompanyAsync(id, companyDto, userId);

                if (updatedCompany == null)
                {
                    return NotFound(new { message = $"Company with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_companies");
                _cache.Remove($"company_{id}");

                return Ok(updatedCompany);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating company with ID {CompanyId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Deletes a company
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Delete a company",
            Description = "Deletes a company by its unique identifier",
            OperationId = "DeleteCompany"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _companyService.DeleteCompanyAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Company with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_companies");
                _cache.Remove($"company_{id}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting company with ID {CompanyId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets all countries
        /// </summary>
        /// <returns>List of countries</returns>
        [HttpGet("countries")]
        [SwaggerOperation(
            Summary = "Get all countries",
            Description = "Retrieves a list of all countries",
            OperationId = "GetAllCountries"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<dynamic>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetCountries()
        {
            try
            {
                var cacheKey = "all_countries";
                if (!_cache.TryGetValue(cacheKey, out var countriesObj) || countriesObj is not IEnumerable<dynamic> countries)
                {
                    countries = await _companyService.GetCountriesAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));
                    _cache.Set(cacheKey, countries, cacheEntryOptions);
                }
                return Ok(countries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving countries");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets states by country ID
        /// </summary>
        /// <param name="countryId">Country ID</param>
        /// <returns>List of states for the specified country</returns>
        [HttpGet("states/{countryId}")]
        [SwaggerOperation(
            Summary = "Get states by country ID",
            Description = "Retrieves a list of states for the specified country",
            OperationId = "GetStatesByCountryId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<dynamic>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetStatesByCountryId(Guid countryId)
        {
            try
            {
                var cacheKey = $"states_{countryId}";
                if (!_cache.TryGetValue(cacheKey, out var statesObj) || statesObj is not IEnumerable<dynamic> states)
                {
                    states = await _companyService.GetStatesByCountryIdAsync(countryId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));
                    _cache.Set(cacheKey, states, cacheEntryOptions);
                }
                return Ok(states);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving states for country ID {CountryId}", countryId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets cities by state ID
        /// </summary>
        /// <param name="stateId">State ID</param>
        /// <returns>List of cities for the specified state</returns>
        [HttpGet("cities/{stateId}")]
        [SwaggerOperation(
            Summary = "Get cities by state ID",
            Description = "Retrieves a list of cities for the specified state",
            OperationId = "GetCitiesByStateId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<dynamic>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetCitiesByStateId(Guid stateId)
        {
            try
            {
                var cacheKey = $"cities_{stateId}";
                if (!_cache.TryGetValue(cacheKey, out var citiesObj) || citiesObj is not IEnumerable<dynamic> cities)
                {
                    cities = await _companyService.GetCitiesByStateIdAsync(stateId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));
                    _cache.Set(cacheKey, cities, cacheEntryOptions);
                }
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving cities for state ID {StateId}", stateId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
