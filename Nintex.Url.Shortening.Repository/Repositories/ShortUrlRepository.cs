using Nintex.Url.Shortening.Core.DbModels;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Repository.DbContext;

namespace Nintex.Url.Shortening.Repository.Repositories
{
    public class ShortUrlRepository : GenericRepository<ShortUrlModel>, IShortUrlRepository
    {
        public ShortUrlRepository(ShortUrlDbContext webProDbContext) : base(webProDbContext)
        {
        }
    }
}
