using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nintex.Url.Shortening.Core.Interfaces.Auth;
using Nintex.Url.Shortening.Core.Interfaces.Services;
using Nintex.Url.Shortening.Core.ViewModels;

namespace Nintex.Url.Shortening.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlService _shortUrlService;
        private readonly ICurrentLoginUser _currentLoginUser;

        public ShortUrlController(IShortUrlService shortUrlService, ICurrentLoginUser currentLoginUser)
        {
            _shortUrlService = shortUrlService;
            _currentLoginUser = currentLoginUser;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var shortUrls = await _shortUrlService.GetAllShortUrlOfAUser(_currentLoginUser.AccountId);
            return Ok(shortUrls);
        }
        
        [HttpGet("logs/{shortUrlId}")]
        public async Task<IActionResult> Get(Int64 shortUrlId)
        {
            var shortUrlLogEntryModels = await _shortUrlService.GetShortUrlLogs(shortUrlId);
            return Ok(shortUrlLogEntryModels);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ShortUrlCreateRequest shortUrlCreateRequest)
        {
            shortUrlCreateRequest.UserId = _currentLoginUser.AccountId;
            shortUrlCreateRequest.HostUrl = GetHostUrl();
            var shortUrlCreateResponse = await _shortUrlService.Create(shortUrlCreateRequest);
            return Ok(shortUrlCreateResponse);
        }
        
        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] ShortUrlRemoveRequest shortUrlRemoveRequest)
        {
            shortUrlRemoveRequest.UserId = _currentLoginUser.AccountId;
            await _shortUrlService.Remove(shortUrlRemoveRequest);
            return Ok();
        }

        #region Supported Methods

        private string GetHostUrl()
        {
            return Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
        }

        #endregion
    }
}