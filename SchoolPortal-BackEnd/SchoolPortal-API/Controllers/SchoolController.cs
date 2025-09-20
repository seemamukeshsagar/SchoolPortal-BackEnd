using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.Models;
using SchoolPortal_API.Services;
using SchoolPortal_API.ViewModels.School;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides school management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISchoolService _schoolService;
        private readonly ILogger<SchoolController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly SchoolNewPortalContext _context;

        public SchoolController(
            ISchoolRepository schoolRepository, 
            ISchoolService schoolService,
            ILogger<SchoolController> logger, 
            IMemoryCache cache,
            IMapper mapper,
            SchoolNewPortalContext context)
        {
            _schoolRepository = schoolRepository ?? throw new ArgumentNullException(nameof(schoolRepository));
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
        /// Gets all active schools with pagination and filtering
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Number of items per page (default: 10, max: 100)</param>
        /// <param name="searchTerm">Optional search term to filter schools</param>
        /// <param name="companyId">Optional filter by company ID</param>
        /// <param name="isActive">Optional filter by active status</param>
        /// <returns>Paginated list of schools</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,SchoolAdmin,Teacher")]
        [SwaggerOperation(
            Summary = "Get all schools",
            Description = "Gets a paginated list of all active schools with optional filtering",
            OperationId = "GetAllSchools"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<SchoolResponseDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllSchools(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = null,
            [FromQuery] Guid? companyId = null,
            [FromQuery] bool? isActive = true)
        {
            try
            {
                // Validate pagination parameters
                pageNumber = Math.Max(1, pageNumber);
                pageSize = Math.Clamp(pageSize, 1, 100);

                var cacheKey = $"schools_page_{pageNumber}_{pageSize}_{searchTerm}_{companyId}_{isActive}";
                
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not PaginatedResponse<SchoolResponseDto> response)
                {
                    // Build the filter expression
                    IQueryable<SchoolMaster> query = _context.SchoolMasters.Where(s => !s.IsDeleted);
                    
                    if (isActive.HasValue)
                    {
                        query = query.Where(s => s.IsActive == isActive.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        var searchTermLower = searchTerm.ToLower();
                        query = query.Where(s => 
                            (s.Name != null && s.Name.ToLower().Contains(searchTermLower)) ||
                            (s.Email != null && s.Email.ToLower().Contains(searchTermLower)));
                    }

                    if (companyId.HasValue)
                    {
                        query = query.Where(s => s.CompanyId == companyId.Value);
                    }

                    // Get total count for pagination
                    var totalCount = await query.CountAsync();
                    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                    // Get paginated data
                    var schools = await query
                        .Include(s => s.Company)
                        .Include(s => s.Country)
                        .Include(s => s.State)
                        .Include(s => s.City)
                        .OrderBy(s => s.Name)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    // Map to DTOs
                    var items = _mapper.Map<IEnumerable<SchoolResponseDto>>(schools);

                    response = new PaginatedResponse<SchoolResponseDto>
                    {
                        Items = items,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalCount = totalCount,
                        TotalPages = totalPages
                    };

                    // Cache the results for 5 minutes
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(cacheKey, response, cacheOptions);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving schools");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving schools" });
            }
        }

        /// <summary>
        /// Gets a school by ID
        /// </summary>
        /// <returns>List of all schools</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all schools",
            Description = "Retrieves a list of all schools",
            OperationId = "GetAllSchools"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SchoolResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllSchools()
        {
            try
            {
                var cacheKey = "all_schools";
                if (!_cache.TryGetValue(cacheKey, out var obj))
                {
                    var schools = await _schoolService.GetAllSchoolsAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, schools, cacheEntryOptions);
                    return Ok(schools);
                }
                var schoolsFromCache = obj as IEnumerable<SchoolResponseDto>;
                return Ok(schoolsFromCache);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving schools");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets schools by company ID
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>List of schools for the specified company</returns>
        [HttpGet("by-company/{companyId}")]
        [SwaggerOperation(
            Summary = "Get schools by company ID",
            Description = "Retrieves a list of schools for the specified company",
            OperationId = "GetSchoolsByCompanyId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SchoolResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetSchoolsByCompanyId(Guid companyId)
        {
            try
            {
                var cacheKey = $"schools_by_company_{companyId}";
                if (!_cache.TryGetValue(cacheKey, out var obj))
                {
                    var schools = await _schoolService.GetSchoolsByCompanyIdAsync(companyId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, schools, cacheEntryOptions);
                    return Ok(schools);
                }
                var schoolsFromCache = obj as IEnumerable<SchoolResponseDto>;
                return Ok(schoolsFromCache);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving schools for company ID {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets a school by ID
        /// </summary>
        /// <param name="id">School ID</param>
        /// <returns>School details</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get school by ID",
            Description = "Retrieves a school by its unique identifier",
            OperationId = "GetSchoolById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SchoolResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetSchoolById(Guid id)
        {
            try
            {
                var cacheKey = $"school_{id}";
                if (!_cache.TryGetValue(cacheKey, out var obj))
                {
                    var school = await _schoolService.GetSchoolByIdAsync(id);
                    if (school != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, school, cacheEntryOptions);
                    }
                    if (school == null)
                    {
                        return NotFound(new { message = $"School with ID {id} not found" });
                    }
                    return Ok(school);
                }
                var schoolFromCache = obj as SchoolResponseDto;
                if (schoolFromCache == null)
                {
                    return NotFound(new { message = $"School with ID {id} not found" });
                }
                return Ok(schoolFromCache);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving school with ID {SchoolId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Creates a new school
        /// </summary>
        /// <param name="schoolDto">School data</param>
        /// <returns>Created school details</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,SchoolAdmin")]
        [SwaggerOperation(
            Summary = "Create a new school",
            Description = "Creates a new school with the provided data",
            OperationId = "CreateSchool"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SchoolResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreateSchool([FromBody] SchoolDto schoolDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                
                // Map DTO to entity
                var school = _mapper.Map<SchoolMaster>(schoolDto);
                
                // Create school using repository
                var createdSchool = await _schoolRepository.CreateAsync(school, userId);
                
                if (createdSchool == null)
                {
                    _logger.LogError("Failed to create school");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create school" });
                }
                
                // Invalidate related caches
                _cache.Remove("all_schools");
                _cache.Remove($"schools_by_company_{createdSchool.CompanyId}");
                _cache.Remove($"school_{createdSchool.Id}");
                
                // Map back to DTO for response
                var responseDto = _mapper.Map<SchoolResponseDto>(createdSchool);
                
                return CreatedAtAction(nameof(GetSchoolById), new { id = responseDto.Id }, responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating school");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Updates an existing school
        /// </summary>
        /// <param name="id">School ID</param>
        /// <param name="schoolDto">Updated school data</param>
        /// <returns>Updated school details</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SchoolAdmin")]
        [SwaggerOperation(
            Summary = "Update a school",
            Description = "Updates an existing school with the provided data",
            OperationId = "UpdateSchool"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SchoolResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateSchool(Guid id, [FromBody] SchoolDto schoolDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var existingSchool = await _schoolRepository.GetByIdAsync(id);
                if (existingSchool == null)
                {
                    return NotFound(new { message = $"School with ID {id} not found" });
                }

                var userId = GetCurrentUserId();
                
                // Map DTO to existing entity
                _mapper.Map(schoolDto, existingSchool);
                
                // Update school using repository
                var updated = await _schoolRepository.UpdateAsync(existingSchool, userId);
                
                if (!updated)
                {
                    _logger.LogError("Failed to update school with ID {SchoolId}", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to update school" });
                }
                
                // Invalidate related caches
                _cache.Remove("all_schools");
                _cache.Remove($"schools_by_company_{existingSchool.CompanyId}");
                _cache.Remove($"school_{id}");
                
                // Refresh the entity to get any database-generated values
                var updatedSchool = await _schoolRepository.GetSchoolWithDetailsAsync(id);
                var responseDto = _mapper.Map<SchoolResponseDto>(updatedSchool);
                
                return Ok(responseDto);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating school with ID {SchoolId}", id);
                return StatusCode(StatusCodes.Status409Conflict, new { message = "The school was modified by another user. Please refresh and try again." });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while updating school with ID {SchoolId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while updating the school" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating school with ID {SchoolId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Deletes a school
        /// </summary>
        /// <param name="id">School ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Delete a school",
            Description = "Performs a soft delete of the specified school",
            OperationId = "DeleteSchool"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteSchool(Guid id)
        {
            try
            {
                var existingSchool = await _schoolRepository.GetByIdAsync(id);
                if (existingSchool == null)
                {
                    return NotFound(new { message = $"School with ID {id} not found" });
                }

                var userId = GetCurrentUserId();
                
                // Perform soft delete
                var deleted = await _schoolRepository.DeleteAsync(id, userId);
                
                if (!deleted)
                {
                    _logger.LogError("Failed to delete school with ID {SchoolId}", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to delete school" });
                }
                
                // Invalidate related caches
                _cache.Remove("all_schools");
                _cache.Remove($"schools_by_company_{existingSchool.CompanyId}");
                _cache.Remove($"school_{id}");
                
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while deleting school with ID {SchoolId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "A database error occurred while deleting the school" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting school with ID {SchoolId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
