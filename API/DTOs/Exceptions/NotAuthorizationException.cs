using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace API.DTOs.Exceptions;
[Serializable]
[ExcludeFromCodeCoverage]
public class NotAuthorizationException : Exception
{
    public NotAuthorizationException()
    {

    }
    public NotAuthorizationException(string message) : base(message)
    {

    }

    public NotAuthorizationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected NotAuthorizationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {

    }
}