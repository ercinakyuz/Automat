namespace Automat.Infrastructure.Common.Contracts
{
    public enum ValidationState
    {
        None = 0,
        Valid = 1,
        NotAcceptable = 2,
        AlreadyExists = 3,
        DoesNotExists = 4,
        UnProcessable = 5,
        PreconditionRequired = 6,
        PreconditionFailed = 7
    }
}