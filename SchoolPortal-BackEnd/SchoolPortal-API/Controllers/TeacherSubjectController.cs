using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherSubject;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides teacher-subject assignment management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/teacher-subjects")]
    [ApiController]
    [Produces("application/json")]
    public class TeacherSubjectController : ControllerBase
    {
        private readonly ITeacherSubjectService _teacherSubjectService;
        private readonly ILogger<TeacherSubjectController> _logger;
        private readonly IMemoryCache _cache;

        public TeacherSubjectController(
            ITeacherSubjectService teacherSubjectService,
            ILogger<TeacherSubjectController> logger,
            IMemoryCache cache)
        {
            _teacherSubjectService = teacherSubjectService ?? throw new ArgumentNullException(nameof(teacherSubjectService));
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
        /// Gets all teacher-subject assignments
        /// </summary>
        /// <returns>List of all teacher-subject assignments</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all teacher-subject assignments",
            Description = "Retrieves all teacher-subject assignments",
            OperationId = "GetAllTeacherSubjects"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherSubjectResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherSubjectResponseDto>>> GetAllTeacherSubjects()
        {
            try
            {
                const string cacheKey = "all_teacher_subjects";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherSubjectResponseDto> teacherSubjects)
                {
                    teacherSubjects = await _teacherSubjectService.GetAllTeacherSubjectsAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherSubjects, cacheEntryOptions);
                }
                return Ok(teacherSubjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all teacher-subject assignments");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving teacher-subject assignments" });
            }
        }

        /// <summary>
        /// Gets a specific teacher-subject assignment by ID
        /// </summary>
        /// <param name="id">Teacher-subject assignment ID</param>
        /// <returns>Teacher-subject assignment details</returns>
        [HttpGet("{id}", Name = "GetTeacherSubjectById")]
        [SwaggerOperation(
            Summary = "Get a teacher-subject assignment by ID",
            Description = "Retrieves a specific teacher-subject assignment by its ID",
            OperationId = "GetTeacherSubjectById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherSubjectResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSubjectResponseDto>> GetTeacherSubjectById(Guid id)
        {
            try
            {
                var cacheKey = $"teacher_subject_{id}";
                if (!_cache.TryGetValue(cacheKey, out TeacherSubjectResponseDto? teacherSubject))
                {
                    teacherSubject = await _teacherSubjectService.GetTeacherSubjectByIdAsync(id);
                    
                    if (teacherSubject != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, teacherSubject, cacheEntryOptions);
                    }
                }

                if (teacherSubject == null)
                {
                    return NotFound(new { message = $"Teacher-subject assignment with ID {id} not found" });
                }

                return Ok(teacherSubject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teacher-subject assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving the teacher-subject assignment" });
            }
        }

        /// <summary>
        /// Gets all subjects assigned to a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <returns>List of subjects assigned to the teacher</returns>
        [HttpGet("teacher/{teacherId}")]
        [SwaggerOperation(
            Summary = "Get subjects by teacher ID",
            Description = "Retrieves all subjects assigned to a specific teacher",
            OperationId = "GetSubjectsByTeacherId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherSubjectResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherSubjectResponseDto>>> GetSubjectsByTeacherId(Guid teacherId)
        {
            try
            {
                var cacheKey = $"teacher_subjects_{teacherId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherSubjectResponseDto> teacherSubjects)
                {
                    teacherSubjects = await _teacherSubjectService.GetSubjectsByTeacherIdAsync(teacherId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherSubjects, cacheEntryOptions);
                }
                return Ok(teacherSubjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subjects for teacher {TeacherId}", teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving teacher's subjects" });
            }
        }

        /// <summary>
        /// Gets all teachers assigned to a subject
        /// </summary>
        /// <param name="subjectId">Subject ID</param>
        /// <returns>List of teachers assigned to the subject</returns>
        [HttpGet("subject/{subjectId}")]
        [SwaggerOperation(
            Summary = "Get teachers by subject ID",
            Description = "Retrieves all teachers assigned to a specific subject",
            OperationId = "GetTeachersBySubjectId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherSubjectResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherSubjectResponseDto>>> GetTeachersBySubjectId(Guid subjectId)
        {
            try
            {
                var cacheKey = $"subject_teachers_{subjectId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherSubjectResponseDto> teacherSubjects)
                {
                    teacherSubjects = await _teacherSubjectService.GetTeachersBySubjectIdAsync(subjectId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherSubjects, cacheEntryOptions);
                }
                return Ok(teacherSubjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teachers for subject {SubjectId}", subjectId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving subject teachers" });
            }
        }

        /// <summary>
        /// Gets all subjects assigned to a teacher for a specific class
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="classId">Class ID</param>
        /// <returns>List of subjects assigned to the teacher for the class</returns>
        [HttpGet("teacher/{teacherId}/class/{classId}")]
        [SwaggerOperation(
            Summary = "Get teacher's subjects by class",
            Description = "Retrieves all subjects assigned to a teacher for a specific class",
            OperationId = "GetTeacherSubjectsByClass"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherSubjectResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherSubjectResponseDto>>> GetTeacherSubjectsByClass(
            Guid teacherId, 
            Guid classId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_class_{classId}_subjects";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherSubjectResponseDto> teacherSubjects)
                {
                    teacherSubjects = await _teacherSubjectService.GetByTeacherAndClassAsync(teacherId, classId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherSubjects, cacheEntryOptions);
                }
                return Ok(teacherSubjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subjects for teacher {TeacherId} in class {ClassId}", teacherId, classId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving teacher's subjects for the class" });
            }
        }

        /// <summary>
        /// Creates a new teacher-subject assignment
        /// </summary>
        /// <param name="teacherSubjectDto">Teacher-subject assignment data</param>
        /// <returns>Created teacher-subject assignment</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Create a new teacher-subject assignment",
            Description = "Creates a new assignment of a teacher to a subject and class",
            OperationId = "CreateTeacherSubject"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherSubjectResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSubjectResponseDto>> CreateTeacherSubject(
            [FromBody] TeacherSubjectDto teacherSubjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var createdTeacherSubject = await _teacherSubjectService.AddTeacherSubjectAsync(teacherSubjectDto, userId);

                if (createdTeacherSubject == null)
                {
                    _logger.LogError("Failed to create teacher-subject assignment");
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                        new { message = "Failed to create teacher-subject assignment" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_subjects");
                _cache.Remove($"teacher_subjects_{teacherSubjectDto.TeacherId}");
                _cache.Remove($"subject_teachers_{teacherSubjectDto.SubjectId}");
                _cache.Remove($"teacher_{teacherSubjectDto.TeacherId}_class_{teacherSubjectDto.ClassMasterId}_subjects");

                return CreatedAtAction(
                    nameof(GetTeacherSubjectById),
                    new { id = createdTeacherSubject.Id },
                    createdTeacherSubject);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already assigned"))
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
                _logger.LogError(ex, "Error creating teacher-subject assignment");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while creating the teacher-subject assignment" });
            }
        }

        /// <summary>
        /// Updates an existing teacher-subject assignment
        /// </summary>
        /// <param name="id">Teacher-subject assignment ID</param>
        /// <param name="teacherSubjectDto">Updated teacher-subject assignment data</param>
        /// <returns>Updated teacher-subject assignment</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Update a teacher-subject assignment",
            Description = "Updates an existing assignment of a teacher to a subject and class",
            OperationId = "UpdateTeacherSubject"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherSubjectResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSubjectResponseDto>> UpdateTeacherSubject(
            Guid id,
            [FromBody] TeacherSubjectDto teacherSubjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedTeacherSubject = await _teacherSubjectService.UpdateTeacherSubjectAsync(
                    id, teacherSubjectDto, userId);

                if (updatedTeacherSubject == null)
                {
                    return NotFound(new { message = $"Teacher-subject assignment with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_subjects");
                _cache.Remove($"teacher_subjects_{teacherSubjectDto.TeacherId}");
                _cache.Remove($"subject_teachers_{teacherSubjectDto.SubjectId}");
                _cache.Remove($"teacher_subject_{id}");
                _cache.Remove($"teacher_{teacherSubjectDto.TeacherId}_class_{teacherSubjectDto.ClassMasterId}_subjects");

                return Ok(updatedTeacherSubject);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already assigned"))
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
                _logger.LogError(ex, "Error updating teacher-subject assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while updating the teacher-subject assignment" });
            }
        }

        /// <summary>
        /// Deletes a teacher-subject assignment
        /// </summary>
        /// <param name="id">Teacher-subject assignment ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Delete a teacher-subject assignment",
            Description = "Deletes a specific teacher-subject assignment by its ID (soft delete)",
            OperationId = "DeleteTeacherSubject"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteTeacherSubject(Guid id)
        {
            try
            {
                // Get the assignment first to get related IDs for cache invalidation
                var assignment = await _teacherSubjectService.GetTeacherSubjectByIdAsync(id);
                if (assignment == null)
                {
                    return NotFound(new { message = $"Teacher-subject assignment with ID {id} not found" });
                }

                var userId = GetCurrentUserId();
                var result = await _teacherSubjectService.RemoveTeacherSubjectAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Teacher-subject assignment with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_subjects");
                _cache.Remove($"teacher_subjects_{assignment.TeacherId}");
                _cache.Remove($"subject_teachers_{assignment.SubjectId}");
                _cache.Remove($"teacher_subject_{id}");
                _cache.Remove($"teacher_{assignment.TeacherId}_class_{assignment.ClassMasterId}_subjects");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher-subject assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while deleting the teacher-subject assignment" });
            }
        }

        /// <summary>
        /// Checks if a teacher is assigned to a specific subject in a class
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="subjectId">Subject ID</param>
        /// <param name="classId">Class ID</param>
        /// <returns>True if the teacher is assigned to the subject in the class, false otherwise</returns>
        [HttpGet("check-assignment")]
        [SwaggerOperation(
            Summary = "Check if teacher is assigned to subject in class",
            Description = "Checks if a specific teacher is assigned to a specific subject in a specific class",
            OperationId = "CheckTeacherAssignedToSubject"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<bool>> CheckTeacherAssignedToSubject(
            [Required] Guid teacherId,
            [Required] Guid subjectId,
            [Required] Guid classId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_subject_{subjectId}_class_{classId}_check";
                if (!_cache.TryGetValue(cacheKey, out bool result))
                {
                    result = await _teacherSubjectService.IsTeacherAssignedToSubjectAsync(teacherId, subjectId, classId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if teacher {TeacherId} is assigned to subject {SubjectId} in class {ClassId}", 
                    teacherId, subjectId, classId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while checking teacher-subject assignment" });
            }
        }
    }
}
