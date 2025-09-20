using SchoolPortal_API.Interfaces;
using SchoolPortal.Shared.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolPortal_API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolNewPortalContext _context;
        private ICompanyRepository? _companies;
        private ISchoolRepository? _schools;
        private ICountryRepository? _countries;
        private IStateRepository? _states;
        private ICityRepository? _cities;
        private ITeacherRepository? _teachers;
        private ITeacherClassRepository? _teacherClasses;
        private ITeacherSectionRepository? _teacherSections;
        private ITeacherSubjectRepository? _teacherSubjects;
        private ITeacherQualificationRepository? _teacherQualifications;
        private ITeacherDocumentRepository? _teacherDocuments;
        private IClassRepository? _classes;
        private ISectionRepository? _sections;
        private IClassSectionRepository? _classSections;

        public UnitOfWork(SchoolNewPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ICompanyRepository Companies => _companies ??= new CompanyRepository(_context);
        public ISchoolRepository Schools => _schools ??= new SchoolRepository(_context);
        public ICountryRepository Countries => _countries ??= new CountryRepository(_context);
        public IStateRepository States => _states ??= new StateRepository(_context);
        public ICityRepository Cities => _cities ??= new CityRepository(_context);
        public ITeacherRepository Teachers => _teachers ??= new TeacherRepository(_context);
        public ITeacherClassRepository TeacherClasses => _teacherClasses ??= new TeacherClassRepository(_context);
        public ITeacherSectionRepository TeacherSections => _teacherSections ??= new TeacherSectionRepository(_context);
        public ITeacherSubjectRepository TeacherSubjects => _teacherSubjects ??= new TeacherSubjectRepository(_context);
        public ITeacherQualificationRepository TeacherQualifications => _teacherQualifications ??= new TeacherQualificationRepository(_context);
        public ITeacherDocumentRepository TeacherDocuments => _teacherDocuments ??= new TeacherDocumentRepository(_context);
        public IClassRepository Classes => _classes ??= new ClassRepository(_context);
        public ISectionRepository Sections => _sections ??= new SectionRepository(_context);
        public IClassSectionRepository ClassSections => _classSections ??= new ClassSectionRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
