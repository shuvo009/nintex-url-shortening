using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ShortUrlCreateRequest shortUrlCreateRequest)
        {
            shortUrlCreateRequest.UserId = _currentLoginUser.AccountId;
            shortUrlCreateRequest.HostUrl = GetHostUrl();
            var shortUrlCreateResponse = await _shortUrlService.Create(shortUrlCreateRequest);
            return Ok(new ServerResponse<ShortUrlCreateResponse> { IsSuccess = true, Data = shortUrlCreateResponse });
        }

        #region Supported Methods

        private string GetHostUrl()
        {
            return Request.Scheme + "://" + Request.Host.Value + Request.PathBase;
        }

        #endregion
    }
}