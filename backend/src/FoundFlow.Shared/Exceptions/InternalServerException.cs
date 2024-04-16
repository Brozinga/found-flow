using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class InternalServerException : CustomException
{
    public InternalServerException(string message)
        : base(message, HttpStatusCode.InternalServerError)
    {
    }

    protected InternalServerException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}