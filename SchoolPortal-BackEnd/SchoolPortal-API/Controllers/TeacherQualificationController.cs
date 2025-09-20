using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherQualification;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides teacher qualification management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/teachers/{teacherId}/qualifications")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TeacherQualificationController : ControllerBase
    {
        private readonly ITeacherQualificationService _qualificationService;
        private readonly ILogger<TeacherQualificationController> _logger;
        private readonly IMemoryCache _cache;

        public TeacherQualificationController(
            ITeacherQualificationService qualificationService, 
            ILogger<TeacherQualificationController> logger, 
            IMemoryCache cache)
        {
            _qualificationService = qualificationService ?? throw new ArgumentNullException(nameof(qualificationService));
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
        /// Gets all qualifications for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="activeOnly">Whether to return only active qualifications (default: false)</param>
        /// <returns>List of teacher's qualifications</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all qualifications for a teacher",
            Description = "Retrieves all qualifications associated with a specific teacher",
            OperationId = "GetTeacherQualifications"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherQualificationResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherQualificationResponseDto>>> GetTeacherQualifications(
            Guid teacherId, 
            [FromQuery] bool activeOnly = false)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_qualifications_{activeOnly}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherQualificationResponseDto> qualifications)
                {
                    qualifications = activeOnly
                        ? await _qualificationService.GetActiveQualificationsByTeacherIdAsync(teacherId)
                        : await _qualificationService.GetQualificationsByTeacherIdAsync(teacherId);
                        
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, qualifications, cacheEntryOptions);
                }
                return Ok(qualifications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving qualifications for teacher {TeacherId}", teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving qualifications" });
            }
        }

        /// <summary>
        /// Gets a specific qualification for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="qualificationId">Qualification ID</param>
        /// <returns>Qualification details</returns>
        [HttpGet("{qualificationId}", Name = "GetTeacherQualificationById")]
        [SwaggerOperation(
            Summary = "Get a teacher qualification by ID",
            Description = "Retrieves a specific qualification for a teacher by its ID",
            OperationId = "GetTeacherQualificationById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherQualificationResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherQualificationResponseDto>> GetTeacherQualificationById(
            Guid teacherId, 
            Guid qualificationId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_qualification_{qualificationId}";
                if (!_cache.TryGetValue(cacheKey, out TeacherQualificationResponseDto? qualification))
                {
                    var qualifications = await _qualificationService.GetQualificationsByTeacherIdAsync(teacherId);
                    qualification = qualifications.FirstOrDefault(q => q.Id == qualificationId);
                    
                    if (qualification != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, qualification, cacheEntryOptions);
                    }
                }

                if (qualification == null)
                {
                    return NotFound(new { message = $"Qualification with ID {qualificationId} not found for teacher {teacherId}" });
                }

                return Ok(qualification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving qualification {QualificationId} for teacher {TeacherId}", 
                    qualificationId, teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving the qualification" });
            }
        }

        /// <summary>
        /// Adds a new qualification for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="qualificationDto">Qualification data</param>
        /// <returns>Created qualification details</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        [SwaggerOperation(
            Summary = "Add a qualification to a teacher",
            Description = "Adds a new qualification to a specific teacher",
            OperationId = "AddTeacherQualification"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherQualificationResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherQualificationResponseDto>> AddTeacherQualification(
            Guid teacherId,
            [FromBody] TeacherQualificationDto qualificationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                // Ensure the teacher ID in the path matches the DTO
                if (qualificationDto.TeacherId != teacherId)
                {
                    return BadRequest(new { message = "Teacher ID in the request body does not match the URL" });
                }

                var userId = GetCurrentUserId();
                var createdQualification = await _qualificationService.AddQualificationAsync(qualificationDto, userId);

                if (createdQualification == null)
                {
                    _logger.LogError("Failed to add qualification for teacher {TeacherId}", teacherId);
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                        new { message = "Failed to add qualification" });
                }

                // Invalidate related caches
                _cache.Remove($"teacher_{teacherId}_qualifications_false");
                _cache.Remove($"teacher_{teacherId}_qualifications_true");

                return CreatedAtAction(
                    nameof(GetTeacherQualificationById),
                    new { teacherId, qualificationId = createdQualification.Id },
                    createdQualification);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already has"))
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
                _logger.LogError(ex, "Error adding qualification for teacher {TeacherId}", teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while adding the qualification" });
            }
        }

        /// <summary>
        /// Updates a qualification for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="qualificationId">Qualification ID</param>
        /// <param name="qualificationDto">Updated qualification data</param>
        /// <returns>Updated qualification details</returns>
        [HttpPut("{qualificationId}")]
        [Authorize(Roles = "Admin,Teacher")]
        [SwaggerOperation(
            Summary = "Update a teacher's qualification",
            Description = "Updates an existing qualification for a teacher",
            OperationId = "UpdateTeacherQualification"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherQualificationResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherQualificationResponseDto>> UpdateTeacherQualification(
            Guid teacherId,
            Guid qualificationId,
            [FromBody] TeacherQualificationDto qualificationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                // Ensure the teacher ID in the path matches the DTO
                if (qualificationDto.TeacherId != teacherId)
                {
                    return BadRequest(new { message = "Teacher ID in the request body does not match the URL" });
                }

                var userId = GetCurrentUserId();
                var updatedQualification = await _qualificationService.UpdateQualificationAsync(
                    qualificationId, qualificationDto, userId);

                if (updatedQualification == null)
                {
                    return NotFound(new { message = $"Qualification with ID {qualificationId} not found" });
                }

                // Invalidate related caches
                _cache.Remove($"teacher_{teacherId}_qualifications_false");
                _cache.Remove($"teacher_{teacherId}_qualifications_true");
                _cache.Remove($"teacher_{teacherId}_qualification_{qualificationId}");

                return Ok(updatedQualification);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already has"))
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
                _logger.LogError(ex, "Error updating qualification {QualificationId} for teacher {TeacherId}", 
                    qualificationId, teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while updating the qualification" });
            }
        }

        /// <summary>
        /// Removes a qualification from a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="qualificationId">Qualification ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{qualificationId}")]
        [Authorize(Roles = "Admin,Teacher")]
        [SwaggerOperation(
            Summary = "Remove a qualification from a teacher",
            Description = "Removes a qualification from a specific teacher (soft delete)",
            OperationId = "RemoveTeacherQualification"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> RemoveTeacherQualification(
            Guid teacherId,
            Guid qualificationId)
        {
            try
            {
                // First verify the qualification belongs to the specified teacher
                var qualifications = await _qualificationService.GetQualificationsByTeacherIdAsync(teacherId);
                var qualification = qualifications.FirstOrDefault(q => q.Id == qualificationId);
                
                if (qualification == null)
                {
                    return NotFound(new { message = $"Qualification with ID {qualificationId} not found for teacher {teacherId}" });
                }

                var userId = GetCurrentUserId();
                var result = await _qualificationService.RemoveQualificationAsync(qualificationId, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Qualification with ID {qualificationId} not found" });
                }

                // Invalidate related caches
                _cache.Remove($"teacher_{teacherId}_qualifications_false");
                _cache.Remove($"teacher_{teacherId}_qualifications_true");
                _cache.Remove($"teacher_{teacherId}_qualification_{qualificationId}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing qualification {QualificationId} from teacher {TeacherId}", 
                    qualificationId, teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while removing the qualification" });
            }
        }
    }
}
