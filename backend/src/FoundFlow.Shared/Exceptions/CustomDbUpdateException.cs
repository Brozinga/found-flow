using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class CustomDbUpdateException : Exception
{
    public CustomDbUpdateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected CustomDbUpdateException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}