using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nintex.Url.Shortening.Core.Interfaces.Auth;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Core.ViewModels;

namespace Nintex.Url.Shortening.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrentLoginUser _currentLoginUser;
        private readonly IUserStore _userStore;

        public UsersController(IUserStore userStore, ICurrentLoginUser currentLoginUser,
            IAccountRepository accountRepository)
        {
            _userStore = userStore;
            _currentLoginUser = currentLoginUser;
            _accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> UserLogin([FromBody] LoginViewModel loginViewModel)
        {
            var loginInfo = await _userStore.Login(loginViewModel);
            return Ok(new ServerResponse<LoginResponse> {IsSuccess = true, Data = loginInfo});
        }

        [HttpPost("signup")]
        public async Task<IActionResult> UserSignUp([FromBody] SignUpViewModel signupViewModel)
        {
            await _userStore.UserSignUp(signupViewModel);
            return Ok(new ServerResponse<object> {IsSuccess = true});
        }
    }
}