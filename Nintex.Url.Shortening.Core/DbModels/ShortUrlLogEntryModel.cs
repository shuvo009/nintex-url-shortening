using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nintex.Url.Shortening.Core.DbModels
{
    public class ShortUrlLogEntryModel : BaseModel
    {
        public long ShortUrlId { get; set; }
        
        [Required]
        public DateTime AccessTimeUtc { get; set; }
        
        [MaxLength(16)]
        public byte[] ClientIp { get; set; }
    }
}
