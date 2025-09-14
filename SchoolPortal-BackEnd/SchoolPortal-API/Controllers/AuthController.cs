using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides authentication endpoints for the School Portal API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token
        /// </summary>
        /// <param name="model">The login credentials</param>
        /// <returns>A JWT token for authenticated requests</returns>
        /// <response code="200">Returns the JWT token and user details</response>
        /// <response code="400">If the request is invalid or missing required fields</response>
        /// <response code="401">If the username or password is incorrect</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Authenticate a user",
            Description = "Authenticates a user and returns a JWT token for authorization",
            OperationId = "AuthenticateUser"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status200OK, "Authentication successful", typeof(LoginResponseViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request data")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Login(
            [FromBody, Required] 
            [SwaggerParameter(Required = true, Description = "User login credentials")]
            LoginViewModel model
        )
        {
            try
            {
                // Input validation
                if (model == null)
                {
                    _logger.LogWarning("Login attempt with null model");
                    return BadRequest(new { message = "Request body cannot be empty" });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Login attempt with invalid model state: {ModelState}", ModelState);
                    return BadRequest(new { 
                        message = "Invalid request data", 
                        errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
                    });
                }

                if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                {
                    _logger.LogWarning("Login attempt with empty username or password");
                    return BadRequest(new { message = "Username and password are required" });
                }

                // Authentication
                var response = await _authService.AuthenticateAsync(model);

                if (response == null)
                {
                    _logger.LogWarning("Failed login attempt for username: {Username}", model.Username);
                    return Unauthorized(new { 
                        message = "Invalid username or password",
                        status = StatusCodes.Status401Unauthorized
                    });
                }

                _logger.LogInformation("User {Username} logged in successfully", model.Username);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for username: {Username}", model?.Username ?? "unknown");
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    new { 
                        message = "An error occurred while processing your request",
                        status = StatusCodes.Status500InternalServerError
                    });
            }
        }
    }
}
