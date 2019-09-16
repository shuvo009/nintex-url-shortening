using Microsoft.Extensions.DependencyInjection;
using Nintex.Url.Shortening.Core.Interfaces.Auth;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Identity;
using Nintex.Url.Shortening.Repository.Repositories;

namespace Nintex.Url.Shortening.DependencyInjection
{
    public class DependencyServiceRegister
    {
        public void Register(IServiceCollection services)
        {
            #region Auth

            services.AddTransient<IUserStore, UserStore>();
            services.AddScoped<ICurrentLoginUser, CurrentLoginUser>();


            #endregion

            #region Services

            #endregion

            #region Repository
            services.AddTransient<IAccountRepository, AccountRepository>();
            #endregion


        }
    }
}
