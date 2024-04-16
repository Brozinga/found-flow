using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FoundFlow.Infrastructure.Middleware;

public class LogResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly string[] _ignoredRoutes;

    public LogResponseMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<LogResponseMiddleware>();
        _ignoredRoutes = new[] { "/sejil" };
    }

    public async Task Invoke(HttpContext context)
    {
        foreach (string path in _ignoredRoutes)
        {
            if (context.Request.Path.StartsWithSegments(path, StringComparison.CurrentCultureIgnoreCase))
            {
                await _next(context);
                return;
            }
        }

        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        await LogResponseBody(context.Response);

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task LogResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        Activity activity = Activity.Current;
        activity?.AddTag("http.response.body", body.MaskSensitiveData());
        string headers = string.Join(", ", response.Headers.Select(h => h.Key + "=" + h.Value).ToArray());
        activity?.AddTag("http.response.headers", headers);

        _logger.LogInformation("{StatusCode} {@Headers} {@Body}", response.StatusCode, headers, body.MaskSensitiveData());
    }
}