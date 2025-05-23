namespace OrderingDomain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base($"Domain error: {message} throws from Domain Layer")
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
