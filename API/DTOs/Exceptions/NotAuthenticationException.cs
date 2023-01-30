using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace API.DTOs.Exceptions;
[Serializable]
[ExcludeFromCodeCoverage]
public class NotAuthenticationException : Exception
{
    public NotAuthenticationException()
    {

    }
    public NotAuthenticationException(string message) : base(message)
    {

    }

    public NotAuthenticationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected NotAuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {

    }
}