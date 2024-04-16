#nullable enable
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public abstract class CustomException : Exception
{
    protected CustomException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }

    protected CustomException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        StatusCode = (HttpStatusCode)(info.GetValue(nameof(StatusCode), typeof(HttpStatusCode)) ?? 500);
    }

    public HttpStatusCode StatusCode { get; }

}