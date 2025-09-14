using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<LoginResponseViewModel?> AuthenticateAsync(LoginViewModel model)
        {
            if (model.Username == null || model.Password == null)
                throw new ArgumentNullException("Username and password are required");

            // Authenticate user with provided credentials
            var user = await _userRepository.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
                return null;

            // Get user roles and permissions
            var userRoles = await _userRepository.GetUserRolesAsync(user.Id) ?? Enumerable.Empty<string>();
            var userPermissions = await _userRepository.GetUserPermissionsAsync(user.Id) ?? Enumerable.Empty<string>();

            // Get designation name
            var designationName = user.Designation?.Name ?? string.Empty;
            var departmentName = user.Company?.CompanyName ?? string.Empty;

            // Prepare claims for the token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.EmailAddress ?? string.Empty),
                new Claim("Department", departmentName),
                new Claim("Designation", designationName)
            };

            // Add roles to claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecret = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
                throw new InvalidOperationException("JWT Secret is not configured");

            var key = Encoding.ASCII.GetBytes(jwtSecret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"] ?? "SchoolPortal",
                Audience = _configuration["Jwt:Audience"] ?? "SchoolPortalClient"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Prepare and return response
            return new LoginResponseViewModel
            {
                Token = tokenString ?? throw new InvalidOperationException("Failed to generate token"),
                Expiration = token.ValidTo,
                UserId = user.Id.ToString(),
                Username = user.UserName ?? string.Empty,
                UserEmail = user.EmailAddress ?? string.Empty,
                DepartmentName = departmentName,
                Designation = designationName,
                Roles = string.Join(",", userRoles),
                Permissions = userPermissions.ToList()
            };
        }
    }
}
