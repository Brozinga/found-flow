using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Behaviors;
using FoundFlow.Shared.Extensions;
using FoundFlow.Shared.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FoundFlow.Application.Common.Behaviours;

[ExcludeFromCodeCoverage]
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly LoggingSettings _loggingSettings;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IOptions<LoggingSettings> loggingSettings)
    {
        ArgumentNullException.ThrowIfNull(loggingSettings);
        _logger = logger;
        _loggingSettings = loggingSettings.Value;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        TRequest requestCopy = default;
        TResponse responseCopy = default;

        if (_loggingSettings.LogRequestEnabled)
        {
            requestCopy = request.DeepCopy();
        }

        TResponse response = await next().ConfigureAwait(false);

        if (_loggingSettings.LogResponseEnabled)
        {
            responseCopy = response.DeepCopy();
        }

        LoggingBehaviorHelper.LogRequestResponse(_logger, typeof(TRequest).Name, requestCopy, responseCopy, null);

        return response;
    }
}