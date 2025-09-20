using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherDocument;
using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class TeacherDocumentService : ITeacherDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherDocumentService> _logger;

        public TeacherDocumentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TeacherDocumentService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TeacherDocumentResponseDto>> GetAllDocumentsAsync()
        {
            var documents = await _unitOfWork.TeacherDocuments.FindAsync(d => !d.IsDeleted);
            return _mapper.Map<IEnumerable<TeacherDocumentResponseDto>>(documents);
        }

        public async Task<TeacherDocumentResponseDto?> GetDocumentByIdAsync(Guid id)
        {
            var document = await _unitOfWork.TeacherDocuments.GetByIdAsync(id);
            if (document == null || document.IsDeleted)
                return null;
                
            return _mapper.Map<TeacherDocumentResponseDto>(document);
        }

        public async Task<IEnumerable<TeacherDocumentResponseDto>> GetDocumentsByTeacherIdAsync(Guid teacherId)
        {
            var documents = await _unitOfWork.TeacherDocuments.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<TeacherDocumentResponseDto>>(documents);
        }

        public async Task<IEnumerable<TeacherDocumentResponseDto>> GetDocumentsBySchoolIdAsync(Guid schoolId)
        {
            var documents = await _unitOfWork.TeacherDocuments.GetBySchoolIdAsync(schoolId);
            return _mapper.Map<IEnumerable<TeacherDocumentResponseDto>>(documents);
        }

        public async Task<IEnumerable<TeacherDocumentResponseDto>> GetDocumentsByCompanyIdAsync(Guid companyId)
        {
            var documents = await _unitOfWork.TeacherDocuments.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<TeacherDocumentResponseDto>>(documents);
        }

        public async Task<TeacherDocumentResponseDto?> CreateDocumentAsync(TeacherDocumentDto documentDto, Guid userId)
        {
            // Check if file name is already in use
            if (await _unitOfWork.TeacherDocuments.FileNameExistsAsync(documentDto.FileName))
            {
                throw new InvalidOperationException("A document with this file name already exists.");
            }

            var document = _mapper.Map<TeacherDocumentDetail>(documentDto);
            document.Id = Guid.NewGuid();
            document.CreatedBy = userId;
            document.CreatedDate = DateTime.UtcNow;
            document.IsActive = true;
            document.IsDeleted = false;

            await _unitOfWork.TeacherDocuments.AddAsync(document);
            await _unitOfWork.CompleteAsync();

            var createdDocument = await _unitOfWork.TeacherDocuments.GetByIdAsync(document.Id);
            return createdDocument == null ? null : _mapper.Map<TeacherDocumentResponseDto>(createdDocument);
        }

        public async Task<TeacherDocumentResponseDto?> UpdateDocumentAsync(Guid id, TeacherDocumentDto documentDto, Guid userId)
        {
            var existingDocument = await _unitOfWork.TeacherDocuments.GetByIdAsync(id);
            if (existingDocument == null || existingDocument.IsDeleted)
                return null;

            // Check if file name is already in use by another document
            if (await _unitOfWork.TeacherDocuments.FileNameExistsAsync(documentDto.FileName, id))
            {
                throw new InvalidOperationException("A document with this file name already exists.");
            }

            _mapper.Map(documentDto, existingDocument);
            existingDocument.ModifiedBy = userId;
            existingDocument.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherDocuments.Update(existingDocument);
            await _unitOfWork.CompleteAsync();

            var updatedDocument = await _unitOfWork.TeacherDocuments.GetByIdAsync(id);
            return updatedDocument == null ? null : _mapper.Map<TeacherDocumentResponseDto>(updatedDocument);
        }

        public async Task<bool> DeleteDocumentAsync(Guid id, Guid userId)
        {
            var document = await _unitOfWork.TeacherDocuments.GetByIdAsync(id);
            if (document == null || document.IsDeleted)
                return false;

            // Soft delete
            document.IsDeleted = true;
            document.ModifiedBy = userId;
            document.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherDocuments.Update(document);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DocumentExistsAsync(Guid id)
        {
            var document = await _unitOfWork.TeacherDocuments.GetByIdAsync(id);
            return document != null && !document.IsDeleted;
        }

        public async Task<bool> FileNameExistsAsync(string fileName, Guid? excludeId = null)
        {
            return await _unitOfWork.TeacherDocuments.FileNameExistsAsync(fileName, excludeId);
        }
    }
}
