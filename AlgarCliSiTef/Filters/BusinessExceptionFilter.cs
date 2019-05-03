using AlgarCliSiTef.http;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using System.Net.Http.Formatting;


namespace AlgarCliSiTef.Filters
{
    public class BusinessExceptionFilter : Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessException)
            {
                BusinessException exception = (BusinessException)context.Exception;

                ResponseFilterContent error = new ResponseFilterContent();
                error.AddMessage(exception.Message, exception.Parameters);

                var jsonFormatter = new JsonMediaTypeFormatter();
                jsonFormatter.SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                };

                context.Result = new ContentResult() { StatusCode = 200, Content = error.ToString()};

                Logger.Error(exception);

            }
            else
            {
                Logger.Error(context.Exception);
            }
        }
    }
}
