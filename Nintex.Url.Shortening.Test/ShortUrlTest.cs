using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nintex.Url.Shortening.Core.Exceptions;
using Nintex.Url.Shortening.Core.Interfaces.Auth;
using Nintex.Url.Shortening.Core.Interfaces.Repository;
using Nintex.Url.Shortening.Core.Interfaces.Services;
using Nintex.Url.Shortening.Core.ViewModels;
using Nintex.Url.Shortening.Identity;
using Nintex.Url.Shortening.Repository.DbContext;
using Nintex.Url.Shortening.Repository.Repositories;
using Nintex.Url.Shortening.Services;
using Nintex.Url.Shortening.Test.MockData;
using NUnit.Framework;

namespace Nintex.Url.Shortening.Test
{
    [TestFixture]
    public class ShortUrlTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup_Dependencies()
        {
            var service = new ServiceCollection();
            service.AddDbContext<ShortUrlDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Test_Short_Url_DB"));

            #region Auth

            service.AddTransient<IUserStore, UserStore>();
            service.AddScoped<ICurrentLoginUser, MockCurrentLoginUser>();


            #endregion

            #region Services
            service.AddTransient<IShortUrlService, ShortUrlService>();
            #endregion

            #region Repository
            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<IShortUrlRepository, ShortUrlRepository>();
            service.AddTransient<IShortUrlLogEntryRepository, ShortUrlLogEntryRepository>();
            #endregion

            _serviceProvider = service.BuildServiceProvider();
        }

        [TearDown]
        public void Down_Dependencies()
        {
            _serviceProvider.Dispose();
        }

        [TestCase]
        public async Task Create_Sort_Url()
        {
            var shortUrlService = _serviceProvider.GetService<IShortUrlService>();
            var currentLoginUser = _serviceProvider.GetService<ICurrentLoginUser>();

            var shortUrlCreateRequest = new ShortUrlCreateRequest
            {
                LongUrl = "https://stackoverflow.com/questions/37724738/how-to-unit-test-asp-net-core-application-with-constructor-dependency-injection",
                HostUrl = "http://127.0.0.1",
                UserId = currentLoginUser.AccountId
            };
            var response = await shortUrlService.Create(shortUrlCreateRequest);

            Assert.NotNull(response.SortUrl);
        }
        
        [TestCase]
        public async Task Get_Same_Sort_Url_For_Same_Long_Url()
        {
            var shortUrlService = _serviceProvider.GetService<IShortUrlService>();
            var currentLoginUser = _serviceProvider.GetService<ICurrentLoginUser>();

            var shortUrlCreateRequest = new ShortUrlCreateRequest
            {
                LongUrl = "https://stackoverflow.com/questions/37724738/how-to-unit-test-asp-net-core-application-with-constructor-dependency-injection",
                HostUrl = "http://127.0.0.1",
                UserId = currentLoginUser.AccountId
            };
            var firstResponse = await shortUrlService.Create(shortUrlCreateRequest);
            var secondResponse = await shortUrlService.Create(shortUrlCreateRequest);

            StringAssert.AreEqualIgnoringCase(firstResponse.SortUrl, secondResponse.SortUrl);
        }

        [TestCase]
        public async Task Generate_Sort_Url_Shorter_Then_Long_Url()
        {
            var shortUrlService = _serviceProvider.GetService<IShortUrlService>();
            var currentLoginUser = _serviceProvider.GetService<ICurrentLoginUser>();

            var shortUrlCreateRequest = new ShortUrlCreateRequest
            {
                LongUrl = "https://stackoverflow.com/questions/37724738/how-to-unit-test-asp-net-core-application-with-constructor-dependency-injection",
                HostUrl = "http://127.0.0.1",
                UserId = currentLoginUser.AccountId
            };
            var response = await shortUrlService.Create(shortUrlCreateRequest);

            Assert.Greater(shortUrlCreateRequest.LongUrl.Length, response.SortUrl.Length);
        }

        [TestCase]
        public async Task Visit_via_Sort_Url()
        {
            var shortUrlService = _serviceProvider.GetService<IShortUrlService>();
            var currentLoginUser = _serviceProvider.GetService<ICurrentLoginUser>();

            var shortUrlCreateRequest = new ShortUrlCreateRequest
            {
                LongUrl = "https://stackoverflow.com/questions/37724738/how-to-unit-test-asp-net-core-application-with-constructor-dependency-injection",
                HostUrl = "http://127.0.0.1",
                UserId = currentLoginUser.AccountId
            };

            var response = await shortUrlService.Create(shortUrlCreateRequest);
            var key = response.SortUrl.Replace($"{shortUrlCreateRequest.HostUrl}/", "");
            var shortUrlModel = await shortUrlService.GetShortUrl(key, "127.0.0.1");

            var logs = await shortUrlService.GetShortUrlLogs(shortUrlModel.Id);

            Assert.AreEqual(1, logs.Count);
            StringAssert.AreEqualIgnoringCase(shortUrlCreateRequest.LongUrl, shortUrlModel.Url);
        }

        [TestCase]
        public void Invalid_Url()
        {
            var shortUrlService = _serviceProvider.GetService<IShortUrlService>();
            var currentLoginUser = _serviceProvider.GetService<ICurrentLoginUser>();

            var shortUrlCreateRequest = new ShortUrlCreateRequest
            {
                LongUrl = "stackoverflow/questions/37724738/how-to-unit-test-asp-net-core-application-with-constructor-dependency-injection",
                HostUrl = "http://127.0.0.1",
                UserId = currentLoginUser.AccountId
            };
            var exception = Assert.ThrowsAsync<Exception>(() => shortUrlService.Create(shortUrlCreateRequest));

            StringAssert.AreEqualIgnoringCase(exception.Message, "Url is not valid");
        }
        
        [TestCase]
        public void Url_Empty()
        {
            var shortUrlService = _serviceProvider.GetService<IShortUrlService>();
            var currentLoginUser = _serviceProvider.GetService<ICurrentLoginUser>();

            var shortUrlCreateRequest = new ShortUrlCreateRequest
            {
                HostUrl = "http://127.0.0.1",
                UserId = currentLoginUser.AccountId
            };
            var exception = Assert.ThrowsAsync<Exception>(() => shortUrlService.Create(shortUrlCreateRequest));

            StringAssert.AreEqualIgnoringCase(exception.Message, "Long url is required");
        }

        [TestCase]
        public async Task Visit_With_Invalid_Key()
        {
            var shortUrlService = _serviceProvider.GetService<IShortUrlService>();
            var currentLoginUser = _serviceProvider.GetService<ICurrentLoginUser>();

            var shortUrlCreateRequest = new ShortUrlCreateRequest
            {
                LongUrl = "https://stackoverflow.com/questions/37724738/how-to-unit-test-asp-net-core-application-with-constructor-dependency-injection",
                HostUrl = "http://127.0.0.1",
                UserId = currentLoginUser.AccountId
            };
            await shortUrlService.Create(shortUrlCreateRequest);

            var exception = Assert.ThrowsAsync<ShortUrlNotFoundException>(() => shortUrlService.GetShortUrl("Invalid_key","127.0.0.1"));

            StringAssert.AreEqualIgnoringCase(exception.Message, "Url is not found");
        }
    }
}
