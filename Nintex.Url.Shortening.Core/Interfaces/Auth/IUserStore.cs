using System.Threading.Tasks;
using Nintex.Url.Shortening.Core.ViewModels;

namespace Nintex.Url.Shortening.Core.Interfaces.Auth
{
    public interface IUserStore
    {
        Task<LoginResponse> Login(LoginViewModel loginViewModel);
        Task UserSignUp(SignUpViewModel loginViewModel);
    }
}
