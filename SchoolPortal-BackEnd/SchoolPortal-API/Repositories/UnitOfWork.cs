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
        private ICountryRepository? _countries;
        private IStateRepository? _states;
        private ICityRepository? _cities;

        public UnitOfWork(SchoolPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ICompanyRepository Companies => _companies ??= new CompanyRepository(_context);
        public ISchoolRepository Schools => _schools ??= new SchoolRepository(_context);
        public ICountryRepository Countries => _countries ??= new CountryRepository(_context);
        public IStateRepository States => _states ??= new StateRepository(_context);
        public ICityRepository Cities => _cities ??= new CityRepository(_context);

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
