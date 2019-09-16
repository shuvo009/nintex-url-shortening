using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Nintex.Url.Shortening.Core.Interfaces.Auth;

namespace Nintex.Url.Shortening.Test.MockData
{
    public class MockCurrentLoginUser : ICurrentLoginUser
    {
        public long AccountId => 1;
        public void SetClaims(IEnumerable<Claim> claims)
        {

        }
    }
}
