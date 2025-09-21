using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Class;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ILogger<ClassController> _logger;
        private readonly IMemoryCache _cache;

        public ClassController(IClassService classService, ILogger<ClassController> logger, IMemoryCache cache)
        {
            _classService = classService ?? throw new ArgumentNullException(nameof(classService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("school/{schoolId}")]
        [SwaggerOperation(Summary = "Get classes by school", Description = "Returns all classes for a given school (excluding soft-deleted)", OperationId = "GetClassesBySchool")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassResponseDto>>> GetBySchool(Guid schoolId)
        {
            try
            {
                var cacheKey = $"classes_school_{schoolId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassResponseDto> items)
                {
                    items = await _classService.GetBySchoolIdAsync(schoolId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting classes by school {SchoolId}", schoolId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("company/{companyId}")]
        [SwaggerOperation(Summary = "Get classes by company", Description = "Returns all classes for a given company (excluding soft-deleted)", OperationId = "GetClassesByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassResponseDto>>> GetByCompany(Guid companyId)
        {
            try
            {
                var cacheKey = $"classes_company_{companyId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassResponseDto> items)
                {
                    items = await _classService.GetByCompanyIdAsync(companyId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting classes by company {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
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

        [HttpGet]
        [SwaggerOperation(Summary = "Get all classes", OperationId = "GetAllClasses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassResponseDto>>> GetAll()
        {
            try
            {
                const string cacheKey = "all_classes";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassResponseDto> items)
                {
                    items = await _classService.GetAllAsync();
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all classes");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get class by ID", OperationId = "GetClassById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ClassResponseDto>> GetById(Guid id)
        {
            try
            {
                var cacheKey = $"class_{id}";
                if (!_cache.TryGetValue(cacheKey, out ClassResponseDto? item))
                {
                    item = await _classService.GetByIdAsync(id);
                    if (item != null)
                    {
                        var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, item, options);
                    }
                }

                if (item == null)
                {
                    return NotFound(new { message = $"Class with ID {id} not found" });
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class by ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Create class", OperationId = "CreateClass")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClassResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ClassResponseDto>> Create([FromBody] ClassDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var created = await _classService.CreateAsync(dto, userId);
                if (created == null)
                {
                    _logger.LogError("Failed to create class");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create class" });
                }

                _cache.Remove("all_classes");
                _cache.Remove($"class_{created.Id}");

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error creating class");
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating class");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update class", OperationId = "UpdateClass")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ClassResponseDto>> Update(Guid id, [FromBody] ClassDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updated = await _classService.UpdateAsync(id, dto, userId);
                if (updated == null)
                {
                    return NotFound(new { message = $"Class with ID {id} not found" });
                }

                _cache.Remove("all_classes");
                _cache.Remove($"class_{id}");

                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error updating class {Id}", id);
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating class {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete class", OperationId = "DeleteClass")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _classService.DeleteAsync(id, userId);
                if (!result)
                {
                    return NotFound(new { message = $"Class with ID {id} not found" });
                }

                _cache.Remove("all_classes");
                _cache.Remove($"class_{id}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting class {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
