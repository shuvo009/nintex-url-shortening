using System.Collections.Generic;
using System.Security.Claims;

namespace Nintex.Url.Shortening.Core.Interfaces.Auth
{
    public interface ICurrentLoginUser
    {
        long AccountId { get; }
        void SetClaims(IEnumerable<Claim> claims);
    }
}
