using System;
using System.Collections.Generic;
using System.Text;

namespace Nintex.Url.Shortening.Core.ViewModels
{
    public class ShortUrlCreateRequest
    {
        public string LongUrl { get; set; }
        public Int64 UserId { get; set; }
        public string HostUrl { get; set; }
    }
}
