using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Nintex.Url.Shortening.Core.Interfaces.Auth;
using Nintex.Url.Shortening.Core.Utility;

namespace Nintex.Url.Shortening.Identity
{
    public class CurrentLoginUser : ICurrentLoginUser
    {
        private List<Claim> _claims;
        public long AccountId
        {
            get
            {
                var accountId = GetClaim(ApplicationVariable.ClaimAccountId);
                return Convert.ToInt64(accountId);
            }
        }

        public void SetClaims(IEnumerable<Claim> claims)
        {
            _claims = claims.ToList();
        }

        private string GetClaim(string type)
        {
            return _claims.SingleOrDefault(c => c.Type == type)?.Value;
        }
    }
}
