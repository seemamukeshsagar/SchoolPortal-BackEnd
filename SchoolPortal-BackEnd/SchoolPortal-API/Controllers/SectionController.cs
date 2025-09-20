using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Section;
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
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;
        private readonly ILogger<SectionController> _logger;
        private readonly IMemoryCache _cache;

        public SectionController(ISectionService sectionService, ILogger<SectionController> logger, IMemoryCache cache)
        {
            _sectionService = sectionService ?? throw new ArgumentNullException(nameof(sectionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet("school/{schoolId}")]
        [SwaggerOperation(Summary = "Get sections by school", OperationId = "GetSectionsBySchool")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SectionResponseDto>>> GetBySchool(Guid schoolId)
        {
            try
            {
                var cacheKey = $"sections_school_{schoolId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<SectionResponseDto> items)
                {
                    items = await _sectionService.GetBySchoolIdAsync(schoolId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sections by school {SchoolId}", schoolId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("company/{companyId}")]
        [SwaggerOperation(Summary = "Get sections by company", OperationId = "GetSectionsByCompany")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SectionResponseDto>>> GetByCompany(Guid companyId)
        {
            try
            {
                var cacheKey = $"sections_company_{companyId}";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<SectionResponseDto> items)
                {
                    items = await _sectionService.GetByCompanyIdAsync(companyId);
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sections by company {CompanyId}", companyId);
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
        [SwaggerOperation(Summary = "Get all sections", OperationId = "GetAllSections")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SectionResponseDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<SectionResponseDto>>> GetAll()
        {
            try
            {
                const string cacheKey = "all_sections";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<SectionResponseDto> items)
                {
                    items = await _sectionService.GetAllAsync();
                    var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, items, options);
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all sections");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get section by ID", OperationId = "GetSectionById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<SectionResponseDto>> GetById(Guid id)
        {
            try
            {
                var cacheKey = $"section_{id}";
                if (!_cache.TryGetValue(cacheKey, out SectionResponseDto? item))
                {
                    item = await _sectionService.GetByIdAsync(id);
                    if (item != null)
                    {
                        var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, item, options);
                    }
                }

                if (item == null)
                {
                    return NotFound(new { message = $"Section with ID {id} not found" });
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting section by ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Create section", OperationId = "CreateSection")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<SectionResponseDto>> Create([FromBody] SectionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var created = await _sectionService.CreateAsync(dto, userId);
                if (created == null)
                {
                    _logger.LogError("Failed to create section");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create section" });
                }

                _cache.Remove("all_sections");
                _cache.Remove($"section_{created.Id}");

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error creating section");
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating section");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update section", OperationId = "UpdateSection")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SectionResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<SectionResponseDto>> Update(Guid id, [FromBody] SectionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                var userId = GetCurrentUserId();
                var updated = await _sectionService.UpdateAsync(id, dto, userId);
                if (updated == null)
                {
                    return NotFound(new { message = $"Section with ID {id} not found" });
                }

                _cache.Remove("all_sections");
                _cache.Remove($"section_{id}");

                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error updating section {Id}", id);
                return Conflict(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating section {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete section", OperationId = "DeleteSection")]
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
                var result = await _sectionService.DeleteAsync(id, userId);
                if (!result)
                {
                    return NotFound(new { message = $"Section with ID {id} not found" });
                }

                _cache.Remove("all_sections");
                _cache.Remove($"section_{id}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting section {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
