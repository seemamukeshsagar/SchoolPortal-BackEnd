using SchoolPortal_API.Interfaces;
using SchoolPortal.Shared.Models;
using System;
using System.Threading.Tasks;

namespace SchoolPortal_API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolPortalContext _context;
        private ICompanyRepository? _companies;
        private ISchoolRepository? _schools;

        public UnitOfWork(SchoolPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ICompanyRepository Companies => _companies ??= new CompanyRepository(_context);
        public ISchoolRepository Schools => _schools ??= new SchoolRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
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
