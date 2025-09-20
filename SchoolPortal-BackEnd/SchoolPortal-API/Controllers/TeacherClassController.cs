using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherClass;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides teacher class assignment management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/teacher-classes")]
    [ApiController]
    [Produces("application/json")]
    public class TeacherClassController : ControllerBase
    {
        private readonly ITeacherClassService _teacherClassService;
        private readonly ILogger<TeacherClassController> _logger;
        private readonly IMemoryCache _cache;

        public TeacherClassController(
            ITeacherClassService teacherClassService,
            ILogger<TeacherClassController> logger,
            IMemoryCache cache)
        {
            _teacherClassService = teacherClassService ?? throw new ArgumentNullException(nameof(teacherClassService));
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
        /// Gets all teacher class assignments
        /// </summary>
        /// <returns>List of all teacher class assignments</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all teacher class assignments",
            Description = "Retrieves all teacher class assignments",
            OperationId = "GetAllTeacherClasses"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherClassResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherClassResponseDto>>> GetAllTeacherClasses()
        {
            try
            {
                const string cacheKey = "all_teacher_classes";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherClassResponseDto> teacherClasses)
                {
                    teacherClasses = await _teacherClassService.GetAllTeacherClassesAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherClasses, cacheEntryOptions);
                }
                return Ok(teacherClasses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all teacher class assignments");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving teacher class assignments" });
            }
        }

        /// <summary>
        /// Gets a specific teacher class assignment by ID
        /// </summary>
        /// <param name="id">Teacher class assignment ID</param>
        /// <returns>Teacher class assignment details</returns>
        [HttpGet("{id}", Name = "GetTeacherClassById")]
        [SwaggerOperation(
            Summary = "Get a teacher class assignment by ID",
            Description = "Retrieves a specific teacher class assignment by its ID",
            OperationId = "GetTeacherClassById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherClassResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherClassResponseDto>> GetTeacherClassById(Guid id)
        {
            try
            {
                var cacheKey = $"teacher_class_{id}";
                if (!_cache.TryGetValue(cacheKey, out TeacherClassResponseDto? teacherClass))
                {
                    teacherClass = await _teacherClassService.GetTeacherClassByIdAsync(id);
                    
                    if (teacherClass != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, teacherClass, cacheEntryOptions);
                    }
                }

                if (teacherClass == null)
                {
                    return NotFound(new { message = $"Teacher class assignment with ID {id} not found" });
                }

                return Ok(teacherClass);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teacher class assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving the teacher class assignment" });
            }
        }

        /// <summary>
        /// Gets all classes assigned to a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <returns>List of classes assigned to the teacher</returns>
        [HttpGet("teacher/{teacherId}")]
        [SwaggerOperation(
            Summary = "Get classes by teacher ID",
            Description = "Retrieves all classes assigned to a specific teacher",
            OperationId = "GetClassesByTeacherId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherClassResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherClassResponseDto>>> GetClassesByTeacherId(Guid teacherId)
        {
            try
            {
                var cacheKey = $"teacher_classes_{teacherId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherClassResponseDto> teacherClasses)
                {
                    teacherClasses = await _teacherClassService.GetClassesByTeacherIdAsync(teacherId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherClasses, cacheEntryOptions);
                }
                return Ok(teacherClasses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving classes for teacher {TeacherId}", teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving teacher's classes" });
            }
        }

        /// <summary>
        /// Gets all teachers assigned to a class
        /// </summary>
        /// <param name="classId">Class ID</param>
        /// <returns>List of teachers assigned to the class</returns>
        [HttpGet("class/{classId}")]
        [SwaggerOperation(
            Summary = "Get teachers by class ID",
            Description = "Retrieves all teachers assigned to a specific class",
            OperationId = "GetTeachersByClassId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherClassResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherClassResponseDto>>> GetTeachersByClassId(Guid classId)
        {
            try
            {
                var cacheKey = $"class_teachers_{classId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherClassResponseDto> teacherClasses)
                {
                    teacherClasses = await _teacherClassService.GetTeachersByClassIdAsync(classId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherClasses, cacheEntryOptions);
                }
                return Ok(teacherClasses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teachers for class {ClassId}", classId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving class teachers" });
            }
        }

        /// <summary>
        /// Creates a new teacher class assignment
        /// </summary>
        /// <param name="teacherClassDto">Teacher class assignment data</param>
        /// <returns>Created teacher class assignment</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Create a new teacher class assignment",
            Description = "Creates a new assignment of a teacher to a class and subject",
            OperationId = "CreateTeacherClass"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherClassResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherClassResponseDto>> CreateTeacherClass(
            [FromBody] TeacherClassDto teacherClassDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var createdTeacherClass = await _teacherClassService.AddTeacherClassAsync(teacherClassDto, userId);

                if (createdTeacherClass == null)
                {
                    _logger.LogError("Failed to create teacher class assignment");
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                        new { message = "Failed to create teacher class assignment" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_classes");
                _cache.Remove($"teacher_classes_{teacherClassDto.TeacherId}");
                _cache.Remove($"class_teachers_{teacherClassDto.ClassId}");

                return CreatedAtAction(
                    nameof(GetTeacherClassById),
                    new { id = createdTeacherClass.Id },
                    createdTeacherClass);
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
                _logger.LogError(ex, "Error creating teacher class assignment");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while creating the teacher class assignment" });
            }
        }

        /// <summary>
        /// Updates an existing teacher class assignment
        /// </summary>
        /// <param name="id">Teacher class assignment ID</param>
        /// <param name="teacherClassDto">Updated teacher class assignment data</param>
        /// <returns>Updated teacher class assignment</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Update a teacher class assignment",
            Description = "Updates an existing assignment of a teacher to a class and subject",
            OperationId = "UpdateTeacherClass"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherClassResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherClassResponseDto>> UpdateTeacherClass(
            Guid id,
            [FromBody] TeacherClassDto teacherClassDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedTeacherClass = await _teacherClassService.UpdateTeacherClassAsync(id, teacherClassDto, userId);

                if (updatedTeacherClass == null)
                {
                    return NotFound(new { message = $"Teacher class assignment with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_classes");
                _cache.Remove($"teacher_classes_{teacherClassDto.TeacherId}");
                _cache.Remove($"class_teachers_{teacherClassDto.ClassId}");
                _cache.Remove($"teacher_class_{id}");

                return Ok(updatedTeacherClass);
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
                _logger.LogError(ex, "Error updating teacher class assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while updating the teacher class assignment" });
            }
        }

        /// <summary>
        /// Deletes a teacher class assignment
        /// </summary>
        /// <param name="id">Teacher class assignment ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Delete a teacher class assignment",
            Description = "Deletes a specific teacher class assignment by its ID (soft delete)",
            OperationId = "DeleteTeacherClass"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteTeacherClass(Guid id)
        {
            try
            {
                // Get the assignment first to get related IDs for cache invalidation
                var assignment = await _teacherClassService.GetTeacherClassByIdAsync(id);
                if (assignment == null)
                {
                    return NotFound(new { message = $"Teacher class assignment with ID {id} not found" });
                }

                var userId = GetCurrentUserId();
                var result = await _teacherClassService.RemoveTeacherClassAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Teacher class assignment with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_classes");
                _cache.Remove($"teacher_classes_{assignment.TeacherId}");
                _cache.Remove($"class_teachers_{assignment.ClassId}");
                _cache.Remove($"teacher_class_{id}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher class assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while deleting the teacher class assignment" });
            }
        }

        /// <summary>
        /// Checks if a teacher teaches in a specific class
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="classId">Class ID</param>
        /// <returns>True if the teacher teaches in the class, false otherwise</returns>
        [HttpGet("check-teacher-class")]
        [SwaggerOperation(
            Summary = "Check if teacher teaches in class",
            Description = "Checks if a specific teacher is assigned to teach in a specific class",
            OperationId = "CheckTeacherTeachesInClass"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<bool>> CheckTeacherTeachesInClass(
            [Required] Guid teacherId,
            [Required] Guid classId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_class_{classId}_check";
                if (!_cache.TryGetValue(cacheKey, out bool result))
                {
                    result = await _teacherClassService.TeacherTeachesInClassAsync(teacherId, classId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if teacher {TeacherId} teaches in class {ClassId}", teacherId, classId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while checking teacher class assignment" });
            }
        }

        /// <summary>
        /// Checks if a teacher teaches a specific subject
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="subjectId">Subject ID</param>
        /// <returns>True if the teacher teaches the subject, false otherwise</returns>
        [HttpGet("check-teacher-subject")]
        [SwaggerOperation(
            Summary = "Check if teacher teaches subject",
            Description = "Checks if a specific teacher is assigned to teach a specific subject",
            OperationId = "CheckTeacherTeachesSubject"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<bool>> CheckTeacherTeachesSubject(
            [Required] Guid teacherId,
            [Required] Guid subjectId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_subject_{subjectId}_check";
                if (!_cache.TryGetValue(cacheKey, out bool result))
                {
                    result = await _teacherClassService.TeacherTeachesSubjectAsync(teacherId, subjectId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if teacher {TeacherId} teaches subject {SubjectId}", teacherId, subjectId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while checking teacher subject assignment" });
            }
        }
    }
}
