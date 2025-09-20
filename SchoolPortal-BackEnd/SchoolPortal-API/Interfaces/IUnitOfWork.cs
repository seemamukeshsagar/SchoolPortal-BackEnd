using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolPortal_API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        ISchoolRepository Schools { get; }
        ICountryRepository Countries { get; }
        IStateRepository States { get; }
        ICityRepository Cities { get; }
        ITeacherRepository Teachers { get; }
        ITeacherClassRepository TeacherClasses { get; }
        ITeacherSectionRepository TeacherSections { get; }
        ITeacherSubjectRepository TeacherSubjects { get; }
        ITeacherQualificationRepository TeacherQualifications { get; }
        ITeacherDocumentRepository TeacherDocuments { get; }
        IClassRepository Classes { get; }
        ISectionRepository Sections { get; }
        IClassSectionRepository ClassSections { get; }
        Task<int> CompleteAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
