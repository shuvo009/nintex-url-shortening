using System;

namespace Nintex.Url.Shortening.Core.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException() : base("Invalid Username or password")
        {
        }
    }
}