using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Automat.Infrastructure.Common.Contracts;
using Automat.Infrastructure.ExceptionHandling.Contracts;
using Automat.Infrastructure.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Automat.Infrastructure.ExceptionHandling.Middlewares
{
    public class LogHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private const string ErrorMessage = "Üzgünüz! İşleminiz sırasında beklenmedik bir hata olustu.";
        public LogHandlerMiddleware(ILogger<LogHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                using (_logger.BeginScope(GetHttpContextDictionary(httpContext)))
                    await Next(httpContext);
            }
            catch
            {
            }
        }
        private Dictionary<string, object> GetHttpContextDictionary(HttpContext httpContext)
        {
            return new Dictionary<string, object>
            {
                {
                  "HttpContext",
                  new Dictionary<string, object>()
                  {
                      ["TraceIdentifier"] = httpContext.TraceIdentifier,
                      ["IpAddress"] = httpContext.Connection.RemoteIpAddress.ToString(),
                      ["Host"] = httpContext.Request.Host.ToString(),
                      ["Path"] = httpContext.Request.Path.ToString(),
                      ["IsHttps"] = httpContext.Request.IsHttps,
                      ["Scheme"] = httpContext.Request.Scheme,
                      ["Method"] = httpContext.Request.Method,
                      ["ContentType"] = httpContext.Request.ContentType,
                      ["Protocol"] = httpContext.Request.Protocol,
                      ["QueryString"] = httpContext.Request.QueryString.ToString(),
                      ["Query"] = httpContext.Request.Query.ToDictionary<KeyValuePair<string, StringValues>, string, string>((Func<KeyValuePair<string, StringValues>, string>) (x => x.Key), (Func<KeyValuePair<string, StringValues>, string>) (y => y.Value.ToString())),
                      ["Headers"] = httpContext.Request.Headers.ToDictionary<KeyValuePair<string, StringValues>, string, string>((Func<KeyValuePair<string, StringValues>, string>) (x => x.Key), (Func<KeyValuePair<string, StringValues>, string>) (y => y.Value.ToString())),
                      ["Cookies"] = httpContext.Request.Cookies.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (x => x.Key), (Func<KeyValuePair<string, string>, string>) (y => y.Value.ToString())),
                      ["StatusCode"] = httpContext.Response.StatusCode
                  }
                }
            };
        }
        private async Task Next(HttpContext context)
        {
            this._logger.LogInformation("[LOG] Request Started " + context.Request.Method + " " + context.Request.GetDisplayUrl());
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(context, ex);
            }
            _logger.LogInformation(
                $"[LOG] Request Ended {context.Request.Method} {context.Response.StatusCode}");
        }
        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var exceptionMessage = exception.Message;
            ValidationException validationException = exception as ValidationException;
            string exceptionCode;
            HttpStatusCode httpStatusCode;
            if (validationException == null)
            {
                BusinessException businessException = exception as BusinessException;
                if (businessException == null)
                {
                    DomainException domainException = exception as DomainException;
                    if (domainException == null)
                    {
                        if (exception is AuthenticationException)
                        {
                            exceptionCode = "EUNAUTH1001";
                            httpStatusCode = HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            exceptionCode = "EUN1001";
                            exceptionMessage = exception.Message;
                            httpStatusCode = HttpStatusCode.InternalServerError;
                        }
                    }
                    else
                    {
                        exceptionCode = domainException.Code;
                        httpStatusCode = HttpStatusCode.BadRequest;
                        exceptionMessage = domainException.Message;
                    }
                }
                else
                {
                    exceptionCode = businessException.Code;
                    httpStatusCode = HttpStatusCode.BadRequest;
                    exceptionMessage = businessException.Message;
                }
            }
            else
            {
                exceptionCode = validationException.Code;
                httpStatusCode = HttpStatusCode.BadRequest;
                exceptionMessage = string.IsNullOrEmpty(validationException.UserFriendlyMessage) ? ErrorMessage : validationException.UserFriendlyMessage;
            }
            if (httpStatusCode == HttpStatusCode.InternalServerError)
                _logger.LogError(exception, "[ERROR] Code(Business): {errorCode} StatusCode: {statusCode} Message: {message}", exceptionCode, httpStatusCode, exceptionMessage);
            else
                _logger.LogError("[ERROR] Code(Business): {errorCode} StatusCode: {statusCode} Message: {message}", exceptionCode, httpStatusCode, exceptionMessage);
            var errorResponse = new ErrorResponse
            {
                Instance = $"urn:automat:{httpStatusCode}:{context.TraceIdentifier}",
                Messages = new List<MessageContract>()
                {
                    new MessageContract
                    {
                        Code = exceptionCode,
                        Content = exception is BusinessException || exception is ValidationException
                            ? exceptionMessage
                            : string.Empty,
                        Title = ErrorMessage,
                        Type = MessageType.Error
                    }
                }
            };
            var responseContent = JsonConvert.SerializeObject(errorResponse, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                }
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            await context.Response.WriteAsync(responseContent);
        }
    }
}