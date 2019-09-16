using System.Threading.Tasks;
using Nintex.Url.Shortening.Core.DbModels;

namespace Nintex.Url.Shortening.Core.Interfaces.Repository
{
    public interface IShortUrlLogEntryRepository : IGenericRepository<ShortUrlLogEntryModel>
    {
        Task Log(long shortUrlId, string remoteIp);
        Task RemoveLogs(long shortUrlId);
    }
}
