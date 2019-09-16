using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly RequestDelegate _next;
        private static readonly ILogger Logger = LogManager.GetLogger("error");
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
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
            var serverResponse = new ServerResponse<object> { IsSuccess = false, Error = exception.Message };

            if (exception is InvalidLoginException)
            {
                code = HttpStatusCode.Unauthorized;
                serverResponse.Error = exception.Message;
            }
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var result = JsonConvert.SerializeObject(serverResponse, serializerSettings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
