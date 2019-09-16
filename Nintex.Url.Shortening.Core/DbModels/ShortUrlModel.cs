using System;
using System.ComponentModel.DataAnnotations;

namespace Nintex.Url.Shortening.Core.DbModels
{
    public class ShortUrlModel : BaseModel
    {
        public string Key { get; set; }

        [Required] public string Url { get; set; }

        [Required] public DateTime CreatedUtc { get; set; }

        public DateTime? ExpiresUtc { get; set; }

        public long CreatorId { get; set; }
    }
}