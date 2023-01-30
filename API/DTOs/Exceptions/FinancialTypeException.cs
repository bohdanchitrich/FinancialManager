using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace API.DTOs.Exceptions;
[Serializable]
[ExcludeFromCodeCoverage]
public class FinancialTypeException : Exception
{
    public FinancialTypeException()
    {

    }
    public FinancialTypeException(string message) : base(message)
    {

    }

    public FinancialTypeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected FinancialTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {

    }
}