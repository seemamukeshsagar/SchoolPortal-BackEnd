using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherDocument;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal_API.Controllers
{
    /// <summary>
    /// Provides teacher document management endpoints for the School Portal API
    /// </summary>
    [Authorize]
    [Route("api/teachers/{teacherId}/documents")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]
    public class TeacherDocumentController : ControllerBase
    {
        private readonly ITeacherDocumentService _documentService;
        private readonly ILogger<TeacherDocumentController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IWebHostEnvironment _env;
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB
        private static readonly string[] AllowedFileExtensions = { ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png" };

        public TeacherDocumentController(
            ITeacherDocumentService documentService, 
            ILogger<TeacherDocumentController> logger, 
            IMemoryCache cache,
            IWebHostEnvironment env)
        {
            _documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _env = env ?? throw new ArgumentNullException(nameof(env));
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
        /// Gets all documents for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <returns>List of teacher's documents</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all documents for a teacher",
            Description = "Retrieves all documents associated with a specific teacher",
            OperationId = "GetTeacherDocuments"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherDocumentResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TeacherDocumentResponseDto>>> GetTeacherDocuments(Guid teacherId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_documents";
                if (!_cache.TryGetValue(cacheKey, out var obj) || obj is not IEnumerable<TeacherDocumentResponseDto> documents)
                {
                    documents = await _documentService.GetDocumentsByTeacherIdAsync(teacherId);
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                    _cache.Set(cacheKey, documents, cacheEntryOptions);
                }
                return Ok(documents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving documents for teacher {TeacherId}", teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Gets a specific document for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="documentId">Document ID</param>
        /// <returns>Document details</returns>
        [HttpGet("{documentId}", Name = "GetTeacherDocumentById")]
        [SwaggerOperation(
            Summary = "Get a teacher document by ID",
            Description = "Retrieves a specific document for a teacher by its ID",
            OperationId = "GetTeacherDocumentById"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDocumentResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherDocumentResponseDto>> GetTeacherDocumentById(Guid teacherId, Guid documentId)
        {
            try
            {
                var cacheKey = $"teacher_{teacherId}_document_{documentId}";
                if (!_cache.TryGetValue(cacheKey, out TeacherDocumentResponseDto? document))
                {
                    var documents = await _documentService.GetDocumentsByTeacherIdAsync(teacherId);
                    document = documents.FirstOrDefault(d => d.Id == documentId);
                    
                    if (document != null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                        _cache.Set(cacheKey, document, cacheEntryOptions);
                    }
                }

                if (document == null)
                {
                    return NotFound(new { message = $"Document with ID {documentId} not found for teacher {teacherId}" });
                }

                return Ok(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving document {DocumentId} for teacher {TeacherId}", documentId, teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Uploads a new document for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="file">The file to upload</param>
        /// <param name="name">Document name</param>
        /// <param name="description">Document description (optional)</param>
        /// <returns>Created document details</returns>
        [HttpPost("upload")]
        [Authorize(Roles = "Admin,Teacher")]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [SwaggerOperation(
            Summary = "Upload a document for a teacher",
            Description = "Uploads a new document for a specific teacher",
            OperationId = "UploadTeacherDocument"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeacherDocumentResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherDocumentResponseDto>> UploadDocument(
            Guid teacherId,
            [Required] IFormFile file,
            [FromForm] string name,
            [FromForm] string? description = null)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { message = "No file uploaded" });
                }

                // Validate file size
                if (file.Length > MaxFileSize)
                {
                    return BadRequest(new { message = $"File size exceeds the maximum limit of {MaxFileSize / (1024 * 1024)}MB" });
                }

                // Validate file extension
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(fileExtension) || !AllowedFileExtensions.Contains(fileExtension))
                {
                    return BadRequest(new { message = $"Invalid file type. Allowed types: {string.Join(", ", AllowedFileExtensions)}" });
                }

                // Generate a unique file name
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "teacher-documents", teacherId.ToString());
                
                // Ensure the directory exists
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Create the document record
                var documentDto = new TeacherDocumentDto
                {
                    TeacherId = teacherId,
                    Name = name,
                    Description = description,
                    FileName = uniqueFileName,
                    // These would typically come from the user's context
                    CompanyId = Guid.Parse("00000000-0000-0000-0000-000000000001"), // Example, replace with actual
                    SchoolId = Guid.Parse("00000000-0000-0000-0000-000000000001")   // Example, replace with actual
                };

                var userId = GetCurrentUserId();
                var createdDocument = await _documentService.CreateDocumentAsync(documentDto, userId);

                if (createdDocument == null)
                {
                    // Clean up the uploaded file if document creation fails
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    
                    _logger.LogError("Failed to create document record for teacher {TeacherId}", teacherId);
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create document record" });
                }

                // Invalidate related caches
                _cache.Remove($"teacher_{teacherId}_documents");

                return CreatedAtRoute(
                    "GetTeacherDocumentById",
                    new { teacherId, documentId = createdDocument.Id },
                    createdDocument);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while uploading document for teacher {TeacherId}", teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Updates a document for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="documentId">Document ID</param>
        /// <param name="documentDto">Document data</param>
        /// <returns>Updated document details</returns>
        [HttpPut("{documentId}")]
        [Authorize(Roles = "Admin,Teacher")]
        [SwaggerOperation(
            Summary = "Update a teacher document",
            Description = "Updates an existing document for a teacher",
            OperationId = "UpdateTeacherDocument"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDocumentResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<TeacherDocumentResponseDto>> UpdateDocument(
            Guid teacherId, 
            Guid documentId, 
            [FromBody] TeacherDocumentDto documentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid request data", errors = ModelState });
                }

                // Ensure the document belongs to the specified teacher
                var existingDocument = (await _documentService.GetDocumentsByTeacherIdAsync(teacherId))
                    .FirstOrDefault(d => d.Id == documentId);
                
                if (existingDocument == null)
                {
                    return NotFound(new { message = $"Document with ID {documentId} not found for teacher {teacherId}" });
                }

                var userId = GetCurrentUserId();
                var updatedDocument = await _documentService.UpdateDocumentAsync(documentId, documentDto, userId);

                if (updatedDocument == null)
                {
                    return NotFound(new { message = $"Document with ID {documentId} not found" });
                }

                // Invalidate related caches
                _cache.Remove($"teacher_{teacherId}_documents");
                _cache.Remove($"teacher_{teacherId}_document_{documentId}");

                return Ok(updatedDocument);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating document {DocumentId} for teacher {TeacherId}", documentId, teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Deletes a document for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="documentId">Document ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{documentId}")]
        [Authorize(Roles = "Admin,Teacher")]
        [SwaggerOperation(
            Summary = "Delete a teacher document",
            Description = "Deletes a specific document for a teacher",
            OperationId = "DeleteTeacherDocument"
        )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteDocument(Guid teacherId, Guid documentId)
        {
            try
            {
                // First, get the document to check ownership and get the file name
                var document = (await _documentService.GetDocumentsByTeacherIdAsync(teacherId))
                    .FirstOrDefault(d => d.Id == documentId);
                
                if (document == null)
                {
                    return NotFound(new { message = $"Document with ID {documentId} not found for teacher {teacherId}" });
                }

                var userId = GetCurrentUserId();
                var result = await _documentService.DeleteDocumentAsync(documentId, userId);

                if (!result)
                {
                    return NotFound(new { message = $"Document with ID {documentId} not found" });
                }

                // Delete the physical file if it exists
                var filePath = Path.Combine(_env.WebRootPath, "uploads", "teacher-documents", teacherId.ToString(), document.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Invalidate related caches
                _cache.Remove($"teacher_{teacherId}_documents");
                _cache.Remove($"teacher_{teacherId}_document_{documentId}");

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting document {DocumentId} for teacher {TeacherId}", documentId, teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Downloads a document for a teacher
        /// </summary>
        /// <param name="teacherId">Teacher ID</param>
        /// <param name="documentId">Document ID</param>
        /// <returns>File download</returns>
        [HttpGet("{documentId}/download")]
        [SwaggerOperation(
            Summary = "Download a teacher document",
            Description = "Downloads a specific document for a teacher",
            OperationId = "DownloadTeacherDocument"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DownloadDocument(Guid teacherId, Guid documentId)
        {
            try
            {
                var document = (await _documentService.GetDocumentsByTeacherIdAsync(teacherId))
                    .FirstOrDefault(d => d.Id == documentId);
                
                if (document == null)
                {
                    return NotFound(new { message = $"Document with ID {documentId} not found for teacher {teacherId}" });
                }

                var filePath = Path.Combine(_env.WebRootPath, "uploads", "teacher-documents", teacherId.ToString(), document.FileName);
                
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(new { message = "The requested file was not found on the server" });
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var contentType = GetContentType(filePath);
                
                return File(fileStream, contentType, document.FileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while downloading document {DocumentId} for teacher {TeacherId}", documentId, teacherId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request" });
            }
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }
    }
}
