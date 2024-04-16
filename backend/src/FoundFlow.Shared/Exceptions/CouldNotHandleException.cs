using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class CouldNotHandleException : Exception
{
    public CouldNotHandleException()
    {
    }

    public CouldNotHandleException(string message)
        : base(message)
    {
    }

    public CouldNotHandleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected CouldNotHandleException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}