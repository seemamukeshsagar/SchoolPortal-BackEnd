using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Teacher;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides teacher management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;
        private readonly IMemoryCache _cache;

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger, IMemoryCache cache)
        {
            _teacherService = teacherService ?? throw new ArgumentNullException(nameof(teacherService));
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
        /// Gets all teachers
        /// </summary>
        /// <returns>List of all teachers</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all teachers",
            Description = "Retrieves a list of all teachers",
            OperationId = "GetAllTeachers"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetAllTeachers()
        {
            try
            {
                var cacheKey = "all_teachers";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<TeacherResponseDto> teachers)
                {
                    teachers = await _teacherService.GetAllTeachersAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teachers, cacheEntryOptions);
                }
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving teachers");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets a teacher by ID
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <returns>Teacher details</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get teacher by ID",
            Description = "Retrieves a teacher by their unique identifier",
            OperationId = "GetTeacherById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherResponseDto>> GetTeacherById(Guid id)
        {
            try
            {
                var cacheKey = $"teacher_{id}";
                if (!_cache.TryGetValue(cacheKey, out TeacherResponseDto? teacher))
                {
                    teacher = await _teacherService.GetTeacherByIdAsync(id);
                    if (teacher != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, teacher, cacheEntryOptions);
                    }
                }
                if (teacher == null)
                {
                    return NotFound(new { message = $"Teacher with ID {id} not found" });
                }
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving teacher with ID {TeacherId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Creates a new teacher
        /// </summary>
        /// <param name="teacherDto">Teacher data</param>
        /// <returns>Created teacher details</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Create a new teacher",
            Description = "Creates a new teacher with the provided data",
            OperationId = "CreateTeacher"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherResponseDto>> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var createdTeacher = await _teacherService.CreateTeacherAsync(teacherDto, userId);

                if (createdTeacher == null)
                {
                    _logger.LogError("Failed to create teacher");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create teacher" });
                }

                // Invalidate related caches
                _cache.Remove("all_teachers");
                if (createdTeacher.Id != Guid.Empty)
                {
                    _cache.Remove($"teacher_{createdTeacher.Id}");
                }

                return CreatedAtAction(nameof(GetTeacherById), new { id = createdTeacher.Id }, createdTeacher);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already in use"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a teacher");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Updates an existing teacher
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <param name="teacherDto">Updated teacher data</param>
        /// <returns>Updated teacher details</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Update a teacher",
            Description = "Updates an existing teacher with the provided data",
            OperationId = "UpdateTeacher"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherResponseDto>> UpdateTeacher(Guid id, [FromBody] TeacherDto teacherDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedTeacher = await _teacherService.UpdateTeacherAsync(id, teacherDto, userId);

                if (updatedTeacher == null)
                {
                    return NotFound(new { message = $"Teacher with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teachers");
                _cache.Remove($"teacher_{id}");

                return Ok(updatedTeacher);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already in use"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating teacher with ID {TeacherId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Deletes a teacher
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Delete a teacher",
            Description = "Deletes a teacher by their unique identifier",
            OperationId = "DeleteTeacher"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _teacherService.DeleteTeacherAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Teacher with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teachers");
                _cache.Remove($"teacher_{id}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting teacher with ID {TeacherId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets teachers by school ID
        /// </summary>
        /// <param name="schoolId">School ID</param>
        /// <returns>List of teachers in the specified school</returns>
        [HttpGet("school/{schoolId}")]
        [SwaggerOperation(
            Summary = "Get teachers by school ID",
            Description = "Retrieves a list of teachers by school ID",
            OperationId = "GetTeachersBySchoolId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetTeachersBySchoolId(Guid schoolId)
        {
            try
            {
                var cacheKey = $"teachers_school_{schoolId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<TeacherResponseDto> teachers)
                {
                    teachers = await _teacherService.GetTeachersBySchoolIdAsync(schoolId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teachers, cacheEntryOptions);
                }
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving teachers for school {SchoolId}", schoolId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets teachers by company ID
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>List of teachers in the specified company</returns>
        [HttpGet("company/{companyId}")]
        [SwaggerOperation(
            Summary = "Get teachers by company ID",
            Description = "Retrieves a list of teachers by company ID",
            OperationId = "GetTeachersByCompanyId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> GetTeachersByCompanyId(Guid companyId)
        {
            try
            {
                var cacheKey = $"teachers_company_{companyId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<TeacherResponseDto> teachers)
                {
                    teachers = await _teacherService.GetTeachersByCompanyIdAsync(companyId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teachers, cacheEntryOptions);
                }
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving teachers for company {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Searches for teachers based on search criteria
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="companyId">Optional company ID filter</param>
        /// <param name="schoolId">Optional school ID filter</param>
        /// <returns>List of matching teachers</returns>
        [HttpGet("search")]
        [SwaggerOperation(
            Summary = "Search teachers",
            Description = "Searches for teachers based on search criteria",
            OperationId = "SearchTeachers"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherResponseDto>>> SearchTeachers(
            [FromQuery, Required(ErrorMessage = "Search term is required")] string searchTerm,
            [FromQuery] Guid? companyId = null,
            [FromQuery] Guid? schoolId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
                {
                    return BadRequest(new { message = "Search term must be at least 2 characters long" });
                }

                var cacheKey = $"teachers_search_{searchTerm}_{companyId}_{schoolId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<TeacherResponseDto> teachers)
                {
                    teachers = await _teacherService.SearchTeachersAsync(searchTerm, companyId, schoolId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(cacheKey, teachers, cacheEntryOptions);
                }
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for teachers with term: {SearchTerm}", searchTerm);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
