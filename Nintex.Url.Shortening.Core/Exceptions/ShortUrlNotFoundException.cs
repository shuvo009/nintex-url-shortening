using System;

namespace Nintex.Url.Shortening.Core.Exceptions
{
    public class ShortUrlNotFoundException : Exception
    {
        public ShortUrlNotFoundException() : base("Url is not found")
        {
        }
    }
}