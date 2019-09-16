using System.Collections.Generic;
using System.Threading.Tasks;
using Nintex.Url.Shortening.Core.DbModels;
using Nintex.Url.Shortening.Core.ViewModels;

namespace Nintex.Url.Shortening.Core.Interfaces.Services
{
    public interface IShortUrlService
    {
        Task<ShortUrlCreateResponse> Create(ShortUrlCreateRequest shortUrlCreateRequest);
        Task Remove(ShortUrlRemoveRequest shortUrlRemoveRequest);
        Task<List<ShortUrlModel>> GetAllShortUrlOfAUser(long accountId);
    }
}