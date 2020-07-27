namespace Automat.Infrastructure.ExceptionHandling.Exceptions
{
    public class ValidationException : CustomExceptionBase
    {
        public string UserFriendlyMessage { get; set; }
    }
}