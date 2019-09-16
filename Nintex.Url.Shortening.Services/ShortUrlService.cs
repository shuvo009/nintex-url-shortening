using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nintex.Url.Shortening.Core.DbModels;
using Nintex.Url.Shortening.Core.Exceptions;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Core.Interfaces.Services;
using Nintex.Url.Shortening.Core.Utility;
using Nintex.Url.Shortening.Core.ViewModels;

namespace Nintex.Url.Shortening.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IShortUrlLogEntryRepository _shortUrlLogEntryRepository;
        private const string ValidChars = "abcdefghjkmnprstwxz2345789";
        private static readonly Random Rnd = new Random();

        public ShortUrlService(IShortUrlRepository shortUrlRepository, IShortUrlLogEntryRepository shortUrlLogEntryRepository)
        {
            _shortUrlRepository = shortUrlRepository;
            _shortUrlLogEntryRepository = shortUrlLogEntryRepository;
        }

        public async Task<ShortUrlCreateResponse> Create(ShortUrlCreateRequest shortUrlCreateRequest)
        {
            shortUrlCreateRequest.LongUrl = shortUrlCreateRequest.LongUrl?.Trim();
            ValidUrlGuard(shortUrlCreateRequest.LongUrl);

            var keyInfo = await GenerateShortKey();
            if (!keyInfo.Success)
                throw new Exception("An internal error occured");

            var shortUrlKey = await GetExistingShortUrl(shortUrlCreateRequest.LongUrl);
            if (string.IsNullOrEmpty(shortUrlKey))
            {
                shortUrlKey = await CreateShortUrl(shortUrlCreateRequest, keyInfo.Key);
            }
            var shortUrlCreateResponse = new ShortUrlCreateResponse
            {
                LongUrl = shortUrlCreateRequest.LongUrl,
                SortUrl = $"{shortUrlCreateRequest.HostUrl}/{shortUrlKey}"
            };

            return shortUrlCreateResponse;
        }

        public async Task Remove(ShortUrlRemoveRequest shortUrlRemoveRequest)
        {
            var shortUrlModel = await _shortUrlRepository.FirstOrDefault(x =>
                x.Id == shortUrlRemoveRequest.Id && x.CreatorId == shortUrlRemoveRequest.UserId);
            if(shortUrlModel == null)
                throw new ShortUrlNotFoundException();
            await _shortUrlRepository.Remove(shortUrlModel);
            await _shortUrlLogEntryRepository.RemoveLogs(shortUrlModel.Id);
        }

        public Task<List<ShortUrlModel>> GetAllShortUrlOfAUser(long accountId)
        {
            return _shortUrlRepository.Find(x => x.CreatorId == accountId);
        }

        public async Task<ShortUrlModel> GetShortUrl(string key, string remoteIp)
        {
            var shortUrlModel = await _shortUrlRepository.FirstOrDefault(x => x.Key == key);
            if(shortUrlModel == null)
                throw new ShortUrlNotFoundException();
            await _shortUrlLogEntryRepository.Log(shortUrlModel.Id, remoteIp);
            return shortUrlModel;
        }

        public Task<List<ShortUrlLogEntryModel>> GetShortUrlLogs(long shortUrlId)
        {
            return _shortUrlLogEntryRepository.Find(x => x.ShortUrlId == shortUrlId);
        }

        #region Supported Methods

        private async Task<(bool Success, string Key)> GenerateShortKey()
        {
            for (int i = 0; i < 100; i++)
            {
                var key = GenerateKey(ApplicationVariable.KeyLength);
                var existing = await _shortUrlRepository.FirstOrDefault(x => x.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase));
                if (existing == null)
                {
                    return (Success: true, Key: key);
                }
            }
            return (Success: false, Key: null);
        }

        private string GenerateKey(int length)
        {
            var ret = new char[length];
            for (var i = 0; i < length; i++)
            {
                int c;
                lock (Rnd)
                    c = Rnd.Next(0, ValidChars.Length);
                ret[i] = ValidChars[c];
            }
            return new string(ret);
        }

        private async Task<string> CreateShortUrl(ShortUrlCreateRequest shortUrlCreateRequest, string key)
        {
            var shortUrlModel = new ShortUrlModel
            {
                Key = key,
                CreatedUtc = DateTime.UtcNow,
                CreatorId = shortUrlCreateRequest.UserId,
                ExpiresUtc = DateTime.UtcNow.AddDays(ApplicationVariable.ShortUrlExpireDays),
                UpdateDate = DateTime.UtcNow,
                Url = shortUrlCreateRequest.LongUrl
            };
            await _shortUrlRepository.Insert(shortUrlModel);
            return shortUrlModel.Key;
        }

        private bool CheckUrl(string url)
        {
            var isValid = Regex.IsMatch(url, ApplicationVariable.UrlPatten);
            return isValid;
        }

        private void ValidUrlGuard(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Long url is required");
            if (!CheckUrl(url))
                throw new Exception("Url is not valid");
        }

        private async Task<string> GetExistingShortUrl(string longUrl)
        {
            var existingShortUrlModel = await _shortUrlRepository.FirstOrDefault(
                    x => x.Url.Equals(longUrl, StringComparison.CurrentCultureIgnoreCase));

            return existingShortUrlModel?.Key;
        }

        #endregion
    }
}