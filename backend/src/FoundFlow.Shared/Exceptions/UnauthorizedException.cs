using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class UnauthorizedException : CustomException
{
    public UnauthorizedException(string message)
        : base(message, HttpStatusCode.Unauthorized)
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}