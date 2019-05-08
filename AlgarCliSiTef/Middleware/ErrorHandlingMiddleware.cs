using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlgarCliSiTef.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ILogger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = JsonConvert.SerializeObject(new { error = ex.Message });

            if (ex is BusinessException)
            {
                BusinessException exception = (BusinessException) ex;
                result = JsonConvert.SerializeObject(new { error = exception.Message });

                code = HttpStatusCode.InternalServerError;
            }

            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is BusinessException) code = HttpStatusCode.BadRequest;
           
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            logger.Error(ex, result);

            return context.Response.WriteAsync(result);
        }
    }
}
