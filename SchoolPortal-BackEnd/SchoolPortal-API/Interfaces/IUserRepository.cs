using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolPortal.Shared.Models;

namespace SchoolPortal_API.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDetail?> AuthenticateAsync(string username, string password);
        Task<UserDetail?> GetByIdAsync(Guid id);
        Task<IEnumerable<string>> GetUserRolesAsync(Guid userId);
        Task<IEnumerable<string>> GetUserPermissionsAsync(Guid userId);
    }
}
