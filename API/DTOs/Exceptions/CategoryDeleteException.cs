using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace API.DTOs.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class CategoryDeleteException : Exception
{

    public CategoryDeleteException()
    {

    }
    public CategoryDeleteException(string message) : base(message)
    {

    }

    public CategoryDeleteException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected CategoryDeleteException(SerializationInfo serializationInfo, StreamingContext streamingContext)
    : base(serializationInfo, streamingContext)
    {

    }
}