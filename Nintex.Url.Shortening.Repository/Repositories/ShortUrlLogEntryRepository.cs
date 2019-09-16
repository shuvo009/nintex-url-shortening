using System;
using System.Linq;
using System.Threading.Tasks;
using Nintex.Url.Shortening.Core.DbModels;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Repository.DbContext;

namespace Nintex.Url.Shortening.Repository.Repositories
{
    public class ShortUrlLogEntryRepository : GenericRepository<ShortUrlLogEntryModel>, IShortUrlLogEntryRepository
    {
        public ShortUrlLogEntryRepository(ShortUrlDbContext shortUrlDbContext) : base(shortUrlDbContext)
        {
        }

        public Task Log(long shortUrlId, string remoteIp)
        {
            var log = new ShortUrlLogEntryModel
            {
                AccessTimeUtc = DateTime.UtcNow,
                ClientIp = remoteIp,
                ShortUrlId = shortUrlId,
                UpdateDate = DateTime.UtcNow
            };
            return Insert(log);
        }

        public Task RemoveLogs(long shortUrlId)
        {
            InternalSet.RemoveRange(InternalSet.Where(x=>x.ShortUrlId == shortUrlId));
            return Save();
        }
    }
}
