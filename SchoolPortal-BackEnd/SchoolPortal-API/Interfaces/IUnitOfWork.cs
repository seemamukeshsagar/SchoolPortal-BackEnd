using System;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        ISchoolRepository Schools { get; }
        Task<int> CompleteAsync();
    }
}
