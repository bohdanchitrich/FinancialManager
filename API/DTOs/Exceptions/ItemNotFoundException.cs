using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace API.DTOs.Exceptions;
[Serializable]
[ExcludeFromCodeCoverage]
public class ItemNotFoundException<T> : Exception
{
    public ItemNotFoundException()
    {

    }
    public ItemNotFoundException(string message) : base(message)
    {

    }

    public ItemNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected ItemNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {

    }
}