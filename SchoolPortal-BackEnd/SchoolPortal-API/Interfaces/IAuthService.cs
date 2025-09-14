using SchoolPortal_API.ViewModels;
using System.Threading.Tasks;

namespace SchoolPortal_API.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseViewModel?> AuthenticateAsync(LoginViewModel model);
    }
}
