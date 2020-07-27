using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Automat.Infrastructure.ExceptionHandling.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerCore(
            this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
            {
                Exception error = context.Features.Get<IExceptionHandlerFeature>().Error;
                ProblemDetails problemDetails = new ProblemDetails
                {
                    Instance = $"urn:checkout:error:{Guid.NewGuid()}"
                };
                if (error is BadHttpRequestException requestException)
                {
                    problemDetails.Title = "Invalid request";
                    var property = typeof(BadHttpRequestException).GetProperty("StatusCode", BindingFlags.Instance | BindingFlags.NonPublic);
                    var status = property != null ? (int)property.GetValue(requestException) : 500;
                    problemDetails.Status = status;
                    problemDetails.Detail = requestException.Message;
                }
                else
                {
                    problemDetails.Title = "An unexpected error occurred!";
                    problemDetails.Status = 500;
                    problemDetails.Detail = error.Demystify().ToString();
                }
                context.Response.StatusCode = problemDetails.Status.Value;

                context.Response.WriteJson(problemDetails, "application/problem+json");
            }));
            return app;
        }

    }
}
