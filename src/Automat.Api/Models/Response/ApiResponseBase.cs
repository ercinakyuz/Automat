using System.Collections.Generic;
using Automat.Infrastructure.ExceptionHandling.Contracts;

namespace Automat.Api.Models.Response
{
    public abstract class ApiResponseBase<TResult>
    {
        public TResult Result { get; set; }

        public string Instance { get; set; }
        public IEnumerable<MessageContract> Messages { get; set; }
    }
}
