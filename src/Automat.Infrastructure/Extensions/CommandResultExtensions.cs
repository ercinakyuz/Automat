using System;
using System.Linq;
using System.Net;
using Automat.Infrastructure.Common.Contracts;
using Automat.Infrastructure.ExceptionHandling.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Automat.Infrastructure.Extensions
{
    public static class CommandResultExtensions
    {
        public static IActionResult PresentFor<TInput>(this TInput input, Func<TInput, dynamic> mapTo = null)
            where TInput : CommandResultBase
        {
            if (input == null)
            {
                return new NoContentResult();
            }
            if (mapTo == null)
            {
                return new ObjectResult(input);
            }
            var output = mapTo(input);
            // valid
            if (input.ValidationState == ValidationState.Valid)
            {
                return new ObjectResult(output);
            }
            // not acceptable
            if (input.ValidationState == ValidationState.NotAcceptable)
            {
                return new ObjectResult(output)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            // already exist
            if (input.ValidationState == ValidationState.AlreadyExists)
            {
                return new ObjectResult(output)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
            }
            // already exist
            if (input.ValidationState == ValidationState.DoesNotExists)
            {
                return new NoContentResult();
            }
            // unprocessable
            if (input.ValidationState == ValidationState.UnProcessable)
            {
                return new ObjectResult(output)
                {
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity
                };
            }
            // 428 PRECONDITION REQUIRED
            if (input.ValidationState == ValidationState.PreconditionRequired)
                return new ObjectResult(output) { StatusCode = (int)HttpStatusCode.PreconditionRequired };
            // 412 PRECONDITION FAILED
            if (input.ValidationState == ValidationState.PreconditionFailed)
                return new ObjectResult(output) { StatusCode = (int)HttpStatusCode.PreconditionFailed };
            if (input.Messages != null && input.Messages.Any() &&
                input.Messages.Any(m => m.Type == MessageType.Information))
            {
                return new ObjectResult(output)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            return new ObjectResult(output);
        }
    }
}
