using System;

namespace Automat.Infrastructure.ExceptionHandling.Exceptions
{
    public abstract class CustomExceptionBase : Exception
    {
        public string Code { get; set; }
    }
}