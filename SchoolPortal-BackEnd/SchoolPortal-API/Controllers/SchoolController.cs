using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.School;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

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
        private readonly ISchoolService _schoolService;
        private readonly ILogger<SchoolController> _logger;

        public SchoolController(ISchoolService schoolService, ILogger<SchoolController> logger)
        {
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        /// Gets all schools
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
                var schools = await _schoolService.GetAllSchoolsAsync();
                return Ok(schools);
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
                var schools = await _schoolService.GetSchoolsByCompanyIdAsync(companyId);
                return Ok(schools);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving schools for company ID {companyId}");
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
                var school = await _schoolService.GetSchoolByIdAsync(id);
                if (school == null)
                {
                    return NotFound(new { message = $"School with ID {id} not found" });
                }
                return Ok(school);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving school with ID {id}");
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
                var createdSchool = await _schoolService.CreateSchoolAsync(schoolDto, userId);
                
                if (createdSchool == null)
                {
                    _logger.LogError("Failed to create school");
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create school" });
                }
                
                return CreatedAtAction(nameof(GetSchoolById), new { id = createdSchool.Id }, createdSchool);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a school");
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

                var userId = GetCurrentUserId();
                var updatedSchool = await _schoolService.UpdateSchoolAsync(id, schoolDto, userId);
                
                if (updatedSchool == null)
                {
                    return NotFound(new { message = $"School with ID {id} not found" });
                }
                
                return Ok(updatedSchool);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating school with ID {id}");
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
            Description = "Deletes a school by its unique identifier",
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
                var userId = GetCurrentUserId();
                var result = await _schoolService.DeleteSchoolAsync(id, userId);
                
                if (!result)
                {
                    return NotFound(new { message = $"School with ID {id} not found" });
                }
                
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting school with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }
    }
}
