using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FoundFlow.Infrastructure.Middleware;

public class LogResponseMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<LogResponseMiddleware>();
    private readonly string[] _ignoredRoutes = new[] { "/graphql" };

    public async Task Invoke(HttpContext context)
    {
        if (_ignoredRoutes.Any(path => context.Request.Path.StartsWithSegments(path, StringComparison.CurrentCultureIgnoreCase)))
        {
            await next(context);
            return;
        }

        Stream originalBodyStream = context.Response.Body;
        await using MemoryStream responseBody = new();
        context.Response.Body = responseBody;

        await next(context);

        await LogResponseBody(context.Response);

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task LogResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        var activity = Activity.Current;
        activity?.AddTag("http.response.body", body.MaskSensitiveData());
        string headers = string.Join(", ", response.Headers.Select(h => h.Key + "=" + h.Value).ToArray());
        activity?.AddTag("http.response.headers", headers);

        _logger.LogInformation("{StatusCode} {@Headers} {@Body}", response.StatusCode, headers, body.MaskSensitiveData());
    }
}