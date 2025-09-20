using SchoolPortal_API.ViewModels.TeacherDocument;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface ITeacherDocumentService
    {
        Task<IEnumerable<TeacherDocumentResponseDto>> GetAllDocumentsAsync();
        Task<TeacherDocumentResponseDto?> GetDocumentByIdAsync(Guid id);
        Task<IEnumerable<TeacherDocumentResponseDto>> GetDocumentsByTeacherIdAsync(Guid teacherId);
        Task<IEnumerable<TeacherDocumentResponseDto>> GetDocumentsBySchoolIdAsync(Guid schoolId);
        Task<IEnumerable<TeacherDocumentResponseDto>> GetDocumentsByCompanyIdAsync(Guid companyId);
        Task<TeacherDocumentResponseDto?> CreateDocumentAsync(TeacherDocumentDto documentDto, Guid userId);
        Task<TeacherDocumentResponseDto?> UpdateDocumentAsync(Guid id, TeacherDocumentDto documentDto, Guid userId);
        Task<bool> DeleteDocumentAsync(Guid id, Guid userId);
        Task<bool> DocumentExistsAsync(Guid id);
        Task<bool> FileNameExistsAsync(string fileName, Guid? excludeId = null);
    }
}
