using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nintex.Url.Shortening.Core.Interfaces.Services;

namespace Nintex.Url.Shortening.Web.Controllers.Web
{
    public class NavigateController : Controller
    {
        private readonly IShortUrlService _shortUrlService;
        private readonly IHttpContextAccessor _accessor;

        public NavigateController(IShortUrlService shortUrlService, IHttpContextAccessor accessor)
        {
            _shortUrlService = shortUrlService;
            _accessor = accessor;
        }

//        [ResponseCache(Duration = 60 * 60 * 48, Location = ResponseCacheLocation.Client)]
//        [HttpGet("/{key}")]
//        public async Task<IActionResult> NavigateTo(string key)
//        {
//            try
//            {
//                var shortUrlModel = await _shortUrlService.GetShortUrl(key, _accessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString());
//                return RedirectPermanent(shortUrlModel.Url);
//            }
//            catch (Exception e)
//            {
//                return View("UrlNotFound", key);
//            }
//        }
    }
}