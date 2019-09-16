using System;
using System.ComponentModel.DataAnnotations;

namespace Nintex.Url.Shortening.Core.DbModels
{
    public class ShortUrlLogEntryModel : BaseModel
    {
        public long ShortUrlId { get; set; }

        [Required] public DateTime AccessTimeUtc { get; set; }

        public string ClientIp { get; set; }
    }
}