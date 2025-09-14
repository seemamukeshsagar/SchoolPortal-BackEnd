using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SchoolPortal_API.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseViewModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
