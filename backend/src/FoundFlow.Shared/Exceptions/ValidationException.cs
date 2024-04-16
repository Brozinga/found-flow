using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace FoundFlow.Shared.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class ValidationException : CustomException
{
    public ValidationException(string message, IDictionary<string, string[]> errors)
        : base(message, HttpStatusCode.BadRequest)
    {
        Errors = errors;
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : base(string.Empty, HttpStatusCode.BadRequest)
    {
        Errors = errors;
    }

    protected ValidationException(SerializationInfo info, StreamingContext context, IDictionary<string, string[]> errors)
        : base(info, context)
    {
        Errors = errors;
    }

    protected ValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public IDictionary<string, string[]> Errors { get; }
}
