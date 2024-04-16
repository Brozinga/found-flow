using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ServiceUnavailableException : CustomException
{
    public ServiceUnavailableException(string message)
        : base(message, HttpStatusCode.ServiceUnavailable)
    {
    }

    protected ServiceUnavailableException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
