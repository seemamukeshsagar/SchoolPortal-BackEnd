using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.ClassSection;
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
    public class ClassSectionController : ControllerBase
    {
        private readonly IClassSectionService _service;
        private readonly ILogger<ClassSectionController> _logger;
        private readonly IMemoryCache _cache;

        public ClassSectionController(IClassSectionService service, ILogger<ClassSectionController> logger, IMemoryCache cache)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
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

        [HttpGet("school/{schoolId}")]
        [SwaggerOperation(Summary = "Get class-section mappings by school", OperationId = "GetClassSectionsBySchool")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassSectionResponseDto>>> GetBySchool(Guid schoolId)
        {
            try
            {
                var cacheKey = $"classsections_school_{schoolId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassSectionResponseDto> items)
                {
                    items = await _service.GetBySchoolIdAsync(schoolId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class-sections by school {SchoolId}", schoolId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("company/{companyId}")]
        [SwaggerOperation(Summary = "Get class-section mappings by company", OperationId = "GetClassSectionsByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassSectionResponseDto>>> GetByCompany(Guid companyId)
        {
            try
            {
                var cacheKey = $"classsections_company_{companyId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassSectionResponseDto> items)
                {
                    items = await _service.GetByCompanyIdAsync(companyId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class-sections by company {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("class/{classId}")]
        [SwaggerOperation(Summary = "Get class-section mappings by class", OperationId = "GetClassSectionsByClass")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassSectionResponseDto>>> GetByClass(Guid classId)
        {
            try
            {
                var cacheKey = $"classsections_class_{classId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassSectionResponseDto> items)
                {
                    items = await _service.GetByClassIdAsync(classId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class-sections by class {ClassId}", classId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("section/{sectionId}")]
        [SwaggerOperation(Summary = "Get class-section mappings by section", OperationId = "GetClassSectionsBySection")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassSectionResponseDto>>> GetBySection(Guid sectionId)
        {
            try
            {
                var cacheKey = $"classsections_section_{sectionId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassSectionResponseDto> items)
                {
                    items = await _service.GetBySectionIdAsync(sectionId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class-sections by section {SectionId}", sectionId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all class-section mappings", OperationId = "GetAllClassSections")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClassSectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<ClassSectionResponseDto>>> GetAll()
        {
            try
            {
                const string cacheKey = "all_classsections";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<ClassSectionResponseDto> items)
                {
                    items = await _service.GetAllAsync();
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all class sections");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get class-section by ID", OperationId = "GetClassSectionById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ClassSectionResponseDto>> GetById(Guid id)
        {
            try
            {
                var cacheKey = $"classsection_{id}";
                if (!_cache.TryGetValue(cacheKey, out ClassSectionResponseDto? item))
                {
                    item = await _service.GetByIdAsync(id);
                    if (item != null)
                    {
                        var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, item, options);
                    }
                }

                if (item == null)
                {
                    return NotFound(new { message = $"ClassSection with ID {id} not found" });
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class section by ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Create class-section mapping", OperationId = "CreateClassSection")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClassSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ClassSectionResponseDto>> Create([FromBody] ClassSectionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var created = await _service.CreateAsync(dto, userId);
                if (created == null)
                {
                    _logger.LogError("Failed to create class-section mapping");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create class-section mapping" });
                }

                _cache.Remove("all_classsections");
                _cache.Remove($"classsection_{created.Id}");

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error creating class-section mapping");
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating class-section mapping");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update class-section mapping", OperationId = "UpdateClassSection")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClassSectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ClassSectionResponseDto>> Update(Guid id, [FromBody] ClassSectionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updated = await _service.UpdateAsync(id, dto, userId);
                if (updated == null)
                {
                    return NotFound(new { message = $"ClassSection with ID {id} not found" });
                }

                _cache.Remove("all_classsections");
                _cache.Remove($"classsection_{id}");

                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error updating class-section mapping {Id}", id);
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating class-section mapping {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete class-section mapping", OperationId = "DeleteClassSection")]
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
                var result = await _service.DeleteAsync(id, userId);
                if (!result)
                {
                    return NotFound(new { message = $"ClassSection with ID {id} not found" });
                }

                _cache.Remove("all_classsections");
                _cache.Remove($"classsection_{id}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting class-section mapping {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
