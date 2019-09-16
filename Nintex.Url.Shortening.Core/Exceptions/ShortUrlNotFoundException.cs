using System;
using System.Collections.Generic;
using System.Text;

namespace Nintex.Url.Shortening.Core.Exceptions
{
    public class ShortUrlNotFoundException : Exception
    {
        public ShortUrlNotFoundException():base("Url is not found")
        {
            
        }
    }
}
