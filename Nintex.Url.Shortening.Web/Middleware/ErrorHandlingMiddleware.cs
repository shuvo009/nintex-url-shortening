using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nintex.Url.Shortening.Core.Exceptions;
using Nintex.Url.Shortening.Core.ViewModels;
using NLog;

namespace Nintex.Url.Shortening.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private static readonly ILogger Logger = LogManager.GetLogger("error");
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Logger.Error(exception);
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is InvalidLoginException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else if (exception is ShortUrlNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}