using System;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        ISchoolRepository Schools { get; }
        ICountryRepository Countries { get; }
        IStateRepository States { get; }
        ICityRepository Cities { get; }
        Task<int> CompleteAsync();
    }
}
