using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;

namespace SchoolPortal_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolNewPortalContext _context;

        public UserRepository(SchoolNewPortalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<UserDetail?> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.UserDetails
                .FirstOrDefaultAsync(u => u.UserName == username);

            // Check if user exists and verify password
            if (user == null || !VerifyPassword(password, user.UserPassword ?? string.Empty))
                return null;

            return user;
        }

        public async Task<UserDetail?> GetByIdAsync(Guid id)
        {
            return await _context.UserDetails
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _context.UserDetails
                .Include(u => u.UserRole)
                    .ThenInclude(r => r!.RolePrivileges)!
                    .ThenInclude(rp => rp.Privilege)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.UserRole?.RolePrivileges == null)
                return Enumerable.Empty<string>();

return user.UserRole.RolePrivileges
                .Where(rp => rp.Privilege?.PrivilegeName != null)
                .Select(rp => rp.Privilege!.PrivilegeName!)
                .Distinct()
                .ToList();
        }

        public async Task<IEnumerable<string>> GetUserPermissionsAsync(Guid userId)
        {
            var user = await _context.UserDetails
                .Include(u => u.UserRole)
                    .ThenInclude(r => r!.RolePrivileges)!
                    .ThenInclude(rp => rp.Privilege)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.UserRole?.RolePrivileges == null)
                return Enumerable.Empty<string>();

            var permissions = new List<string>();

            // Add role-based permissions
            permissions.AddRange(
                user.UserRole.RolePrivileges
                    .Where(rp => rp.Privilege?.PrivilegeName != null)
                    .Select(rp => rp.Privilege!.PrivilegeName!)
                    .Distinct()
            );

            // Add super user permission if applicable
            if (user.IsSuperUser == true)
            {
                permissions.Add("*"); // Wildcard for all permissions
            }

            // Ensure we return IEnumerable<string> without null values
            return permissions
                .Where(p => p != null)
                .Select(p => p!)
                .Distinct()
                .ToList()
                .AsReadOnly();
        }

        private bool VerifyPassword(string password, string storedPassword)
        {
            // In a real application, you should use proper password hashing
            // For now, this is a simple comparison (not secure for production)
            return password == storedPassword;
        }
    }
}
