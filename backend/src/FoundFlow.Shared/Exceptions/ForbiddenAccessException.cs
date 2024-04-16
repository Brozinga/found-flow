using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ForbiddenAccessException : CustomException
{
    public ForbiddenAccessException(string message)
        : base(message, HttpStatusCode.Forbidden)
    {
    }

    protected ForbiddenAccessException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
