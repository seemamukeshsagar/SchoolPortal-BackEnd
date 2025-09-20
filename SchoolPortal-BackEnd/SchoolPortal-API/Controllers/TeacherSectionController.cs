using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherSection;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides teacher-section assignment management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/teacher-sections")]
    [ApiController]
    [Produces("application/json")]
    public class TeacherSectionController : ControllerBase
    {
        private readonly ITeacherSectionService _teacherSectionService;
        private readonly ILogger<TeacherSectionController> _logger;
        private readonly IMemoryCache _cache;

        public TeacherSectionController(
            ITeacherSectionService teacherSectionService,
            ILogger<TeacherSectionController> logger,
            IMemoryCache cache)
        {
            _teacherSectionService = teacherSectionService ?? throw new ArgumentNullException(nameof(teacherSectionService));
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
        /// Gets all teacher-section assignments
        /// </summary>
        /// <returns>List of all teacher-section assignments</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all teacher-section assignments",
            Description = "Retrieves all teacher-section assignments",
            OperationId = "GetAllTeacherSections"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherSectionResponseDto>>> GetAllTeacherSections()
        {
            try
            {
                const string cacheKey = "all_teacher_sections";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherSectionResponseDto> teacherSections)
                {
                    teacherSections = await _teacherSectionService.GetAllTeacherSectionsAsync();
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherSections, cacheEntryOptions);
                }
                return Ok(teacherSections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all teacher-section assignments");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving teacher-section assignments" });
            }
        }

        /// <summary>
        /// Gets a specific teacher-section assignment by ID
        /// </summary>
        /// <param name="id">Teacher-section assignment ID</param>
        /// <returns>Teacher-section assignment details</returns>
        [HttpGet("{id}", Name = "GetTeacherSectionById")]
        [SwaggerOperation(
            Summary = "Get a teacher-section assignment by ID",
            Description = "Retrieves a specific teacher-section assignment by its ID",
            OperationId = "GetTeacherSectionById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSectionResponseDto>> GetTeacherSectionById(Guid id)
        {
            try
            {
                var cacheKey = $"teacher_section_{id}";
                if (!_cache.TryGetValue(cacheKey, out TeacherSectionResponseDto? teacherSection))
                {
                    teacherSection = await _teacherSectionService.GetTeacherSectionByIdAsync(id);
                    
                    if (teacherSection != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, teacherSection, cacheEntryOptions);
                    }
                }

                if (teacherSection == null)
                {
                    return NotFound(new { message = $"Teacher-section assignment with ID {id} not found" });
                }

                return Ok(teacherSection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teacher-section assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving the teacher-section assignment" });
            }
        }

        /// <summary>
        /// Gets all sections assigned to a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <returns>List of sections assigned to the teacher</returns>
        [HttpGet("teacher/{teacherId}")]
        [SwaggerOperation(
            Summary = "Get sections by teacher ID",
            Description = "Retrieves all sections assigned to a specific teacher",
            OperationId = "GetSectionsByTeacherId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherSectionResponseDto>>> GetSectionsByTeacherId(Guid teacherId)
        {
            try
            {
                var cacheKey = $"teacher_sections_{teacherId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherSectionResponseDto> teacherSections)
                {
                    teacherSections = await _teacherSectionService.GetSectionsByTeacherIdAsync(teacherId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherSections, cacheEntryOptions);
                }
                return Ok(teacherSections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sections for teacher {TeacherId}", teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving teacher's sections" });
            }
        }

        /// <summary>
        /// Gets all teachers assigned to a section
        /// </summary>
        /// <param name="sectionId">Section ID</param>
        /// <returns>List of teachers assigned to the section</returns>
        [HttpGet("section/{sectionId}")]
        [SwaggerOperation(
            Summary = "Get teachers by section ID",
            Description = "Retrieves all teachers assigned to a specific section",
            OperationId = "GetTeachersBySectionId"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherSectionResponseDto>>> GetTeachersBySectionId(Guid sectionId)
        {
            try
            {
                var cacheKey = $"section_teachers_{sectionId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || 
                    obj is not IEnumerable<TeacherSectionResponseDto> teacherSections)
                {
                    teacherSections = await _teacherSectionService.GetTeachersBySectionIdAsync(sectionId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, teacherSections, cacheEntryOptions);
                }
                return Ok(teacherSections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving teachers for section {SectionId}", sectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving section teachers" });
            }
        }

        /// <summary>
        /// Gets the class teacher for a specific class and section
        /// </summary>
        /// <param name="classId">Class ID</param>
        /// <param name="sectionId">Section ID</param>
        /// <returns>Class teacher details</returns>
        [HttpGet("class-teacher/{classId}/{sectionId}")]
        [SwaggerOperation(
            Summary = "Get class teacher by class and section",
            Description = "Retrieves the class teacher for a specific class and section",
            OperationId = "GetClassTeacher"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSectionResponseDto>> GetClassTeacher(Guid classId, Guid sectionId)
        {
            try
            {
                var cacheKey = $"class_teacher_{classId}_{sectionId}";
                if (!_cache.TryGetValue(cacheKey, out TeacherSectionResponseDto? classTeacher))
                {
                    classTeacher = await _teacherSectionService.GetClassTeacherAsync(classId, sectionId);
                    
                    if (classTeacher != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, classTeacher, cacheEntryOptions);
                    }
                }

                if (classTeacher == null)
                {
                    return NotFound(new { message = "No class teacher found for the specified class and section" });
                }

                return Ok(classTeacher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving class teacher for class {ClassId} and section {SectionId}", classId, sectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving the class teacher" });
            }
        }

        /// <summary>
        /// Creates a new teacher-section assignment
        /// </summary>
        /// <param name="teacherSectionDto">Teacher-section assignment data</param>
        /// <returns>Created teacher-section assignment</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Create a new teacher-section assignment",
            Description = "Creates a new assignment of a teacher to a section and subject",
            OperationId = "CreateTeacherSection"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSectionResponseDto>> CreateTeacherSection(
            [FromBody] TeacherSectionDto teacherSectionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var createdTeacherSection = await _teacherSectionService.AddTeacherSectionAsync(teacherSectionDto, userId);

                if (createdTeacherSection == null)
                {
                    _logger.LogError("Failed to create teacher-section assignment");
                    return StatusCode(StatusCodes.Status500InternalServerError, 
                        new { message = "Failed to create teacher-section assignment" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_sections");
                _cache.Remove($"teacher_sections_{teacherSectionDto.TeacherId}");
                _cache.Remove($"section_teachers_{teacherSectionDto.SectionId}");
                
                if (teacherSectionDto.IsClassTeacher)
                {
                    _cache.Remove($"class_teacher_{teacherSectionDto.ClassId}_{teacherSectionDto.SectionId}");
                }

                return CreatedAtAction(
                    nameof(GetTeacherSectionById),
                    new { id = createdTeacherSection.Id },
                    createdTeacherSection);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already assigned") || 
                                                     ex.Message.Contains("already a class teacher"))
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
                _logger.LogError(ex, "Error creating teacher-section assignment");
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while creating the teacher-section assignment" });
            }
        }

        /// <summary>
        /// Updates an existing teacher-section assignment
        /// </summary>
        /// <param name="id">Teacher-section assignment ID</param>
        /// <param name="teacherSectionDto">Updated teacher-section assignment data</param>
        /// <returns>Updated teacher-section assignment</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Update a teacher-section assignment",
            Description = "Updates an existing assignment of a teacher to a section and subject",
            OperationId = "UpdateTeacherSection"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSectionResponseDto>> UpdateTeacherSection(
            Guid id,
            [FromBody] TeacherSectionDto teacherSectionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updatedTeacherSection = await _teacherSectionService.UpdateTeacherSectionAsync(
                    id, teacherSectionDto, userId);

                if (updatedTeacherSection == null)
                {
                    return NotFound(new { message = $"Teacher-section assignment with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_sections");
                _cache.Remove($"teacher_sections_{teacherSectionDto.TeacherId}");
                _cache.Remove($"section_teachers_{teacherSectionDto.SectionId}");
                _cache.Remove($"teacher_section_{id}");
                
                if (teacherSectionDto.IsClassTeacher)
                {
                    _cache.Remove($"class_teacher_{teacherSectionDto.ClassId}_{teacherSectionDto.SectionId}");
                }

                return Ok(updatedTeacherSection);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already assigned") || 
                                                     ex.Message.Contains("already a class teacher"))
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
                _logger.LogError(ex, "Error updating teacher-section assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while updating the teacher-section assignment" });
            }
        }

        /// <summary>
        /// Deletes a teacher-section assignment
        /// </summary>
        /// <param name="id">Teacher-section assignment ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Delete a teacher-section assignment",
            Description = "Deletes a specific teacher-section assignment by its ID (soft delete)",
            OperationId = "DeleteTeacherSection"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteTeacherSection(Guid id)
        {
            try
            {
                // Get the assignment first to get related IDs for cache invalidation
                var assignment = await _teacherSectionService.GetTeacherSectionByIdAsync(id);
                if (assignment == null)
                {
                    return NotFound(new { message = $"Teacher-section assignment with ID {id} not found" });
                }

                var userId = GetCurrentUserId();
                var result = await _teacherSectionService.RemoveTeacherSectionAsync(id, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Teacher-section assignment with ID {id} not found" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_sections");
                _cache.Remove($"teacher_sections_{assignment.TeacherId}");
                _cache.Remove($"section_teachers_{assignment.SectionId}");
                _cache.Remove($"teacher_section_{id}");
                
                if (assignment.IsClassTeacher)
                {
                    _cache.Remove($"class_teacher_{assignment.ClassId}_{assignment.SectionId}");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot remove a class teacher"))
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher-section assignment {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while deleting the teacher-section assignment" });
            }
        }

        /// <summary>
        /// Sets a teacher as the class teacher for a specific class and section
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="classId">Class ID</param>
        /// <param name="sectionId">Section ID</param>
        /// <returns>Updated class teacher details</returns>
        [HttpPost("set-class-teacher/{teacherId}/{classId}/{sectionId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
            Summary = "Set class teacher",
            Description = "Sets a teacher as the class teacher for a specific class and section",
            OperationId = "SetClassTeacher"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherSectionResponseDto>> SetClassTeacher(
            Guid teacherId, 
            Guid classId, 
            Guid sectionId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _teacherSectionService.SetClassTeacherAsync(teacherId, classId, sectionId, userId);

                if (!result)
                {
                    return BadRequest(new { message = "Failed to set class teacher" });
                }

                // Invalidate related caches
                _cache.Remove("all_teacher_sections");
                _cache.Remove($"teacher_sections_{teacherId}");
                _cache.Remove($"section_teachers_{sectionId}");
                _cache.Remove($"class_teacher_{classId}_{sectionId}");

                // Get the updated class teacher
                var classTeacher = await _teacherSectionService.GetClassTeacherAsync(classId, sectionId);
                return Ok(classTeacher);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting class teacher {TeacherId} for class {ClassId} and section {SectionId}", 
                    teacherId, classId, sectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while setting the class teacher" });
            }
        }

        /// <summary>
        /// Checks if a teacher is assigned to a specific section
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="sectionId">Section ID</param>
        /// <returns>True if the teacher is assigned to the section, false otherwise</returns>
        [HttpGet("check-teacher-section")]
        [SwaggerOperation(
            Summary = "Check if teacher is assigned to section",
            Description = "Checks if a specific teacher is assigned to a specific section",
            OperationId = "CheckTeacherTeachesInSection"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<bool>> CheckTeacherTeachesInSection(
            [Required] Guid teacherId,
            [Required] Guid sectionId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_section_{sectionId}_check";
                if (!_cache.TryGetValue(cacheKey, out bool result))
                {
                    result = await _teacherSectionService.TeacherTeachesInSectionAsync(teacherId, sectionId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if teacher {TeacherId} is assigned to section {SectionId}", teacherId, sectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while checking teacher-section assignment" });
            }
        }

        /// <summary>
        /// Checks if a teacher is the class teacher for a specific class and section
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="classId">Class ID</param>
        /// <param name="sectionId">Section ID</param>
        /// <returns>True if the teacher is the class teacher, false otherwise</returns>
        [HttpGet("check-class-teacher")]
        [SwaggerOperation(
            Summary = "Check if teacher is class teacher",
            Description = "Checks if a specific teacher is the class teacher for a specific class and section",
            OperationId = "CheckIsClassTeacher"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<bool>> CheckIsClassTeacher(
            [Required] Guid teacherId,
            [Required] Guid classId,
            [Required] Guid sectionId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_class_{classId}_section_{sectionId}_check";
                if (!_cache.TryGetValue(cacheKey, out bool result))
                {
                    result = await _teacherSectionService.IsClassTeacherAsync(teacherId, classId, sectionId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                    _cache.Set(cacheKey, result, cacheEntryOptions);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if teacher {TeacherId} is class teacher for class {ClassId} and section {SectionId}", 
                    teacherId, classId, sectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while checking class teacher status" });
            }
        }
    }
}
