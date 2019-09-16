using Nintex.Url.Shortening.Core.DbModels;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Repository.DbContext;

namespace Nintex.Url.Shortening.Repository.Repositories
{
    public class ShortUrlLogEntryRepository : GenericRepository<ShortUrlLogEntryModel>, IShortUrlLogEntryRepository
    {
        public ShortUrlLogEntryRepository(ShortUrlDbContext webProDbContext) : base(webProDbContext)
        {
        }
    }
}
