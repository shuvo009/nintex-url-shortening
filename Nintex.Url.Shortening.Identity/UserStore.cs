using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Nintex.Url.Shortening.Core.DbModels;
using Nintex.Url.Shortening.Core.Exceptions;
using Nintex.Url.Shortening.Core.Interfaces.Auth;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Core.Utility;
using Nintex.Url.Shortening.Core.ViewModels;

namespace Nintex.Url.Shortening.Identity
{
    public class UserStore : IUserStore
    {
        private readonly IAccountRepository _accountRepository;

        public UserStore(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public async Task<LoginResponse> Login(LoginViewModel loginViewModel)
        {
            if (String.IsNullOrEmpty(loginViewModel.Username) || String.IsNullOrEmpty(loginViewModel.Password))
                throw new InvalidLoginException();

            loginViewModel.Username = loginViewModel.Username.ToLower().Trim();
            var account = await _accountRepository.FirstOrDefault(x => x.Username.ToLower() == loginViewModel.Username);
            if (account == null)
                throw new InvalidLoginException();

            var isCorrectPassword = PasswordHasher.VerifyPasswordHash(loginViewModel.Password, account.PasswordHash, account.PasswordSalt);
            if (!isCorrectPassword)
                throw new InvalidLoginException();
            var loginResponse = new LoginResponse
            {
                Token = GenerateToken(account),
                Name = account.Name,
            };
            return loginResponse;
        }

        public async Task UserSignUp(SignUpViewModel signUpViewModel)
        {
            if (String.IsNullOrEmpty(signUpViewModel.Username) || String.IsNullOrEmpty(signUpViewModel.Password) || String.IsNullOrEmpty(signUpViewModel.ConfirmPassword))
                throw new Exception(ApplicationVariable.RequiredFieldsMessage);

            if (!signUpViewModel.Password.Equals(signUpViewModel.ConfirmPassword))
                throw new Exception(ApplicationVariable.PasswordDoesNotMatch);

            var account = await _accountRepository.Find(x => x.Username.ToLower() == signUpViewModel.Username);
            if (account != null)
                throw new Exception(ApplicationVariable.UserAlreadyExist);

            var accountModel = new AccountModel { Username = signUpViewModel.Username };
            PasswordHasher.CreatePasswordHash(signUpViewModel.Password, out var passwordHash, out var passwordSalt);
            accountModel.PasswordHash = passwordHash;
            accountModel.PasswordSalt = passwordSalt;
            await _accountRepository.Insert(accountModel);
        }


        #region Private Methods

        public string GenerateToken(AccountModel account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ApplicationVariable.JwtSigninKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ApplicationVariable.ClaimAccountId, account.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        #endregion
    }
}
