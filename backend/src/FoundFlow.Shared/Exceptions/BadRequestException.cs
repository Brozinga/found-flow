using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class BadRequestException : CustomException
{
    public BadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest)
    {
    }

    protected BadRequestException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}