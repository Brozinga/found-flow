using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FoundFlow.Infrastructure.Middleware;

public class LogRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly string[] _ignoredRoutes;

    public LogRequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<LogRequestMiddleware>();
        _ignoredRoutes = new[] { "/dashboard", "/swagger" };  // Adicione todas as rotas que deseja ignorar aqui
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

        context.Request.EnableBuffering();

        await LogRequestBody(context.Request);

        await _next(context);
    }

    private async Task LogRequestBody(HttpRequest demand)
    {
        demand.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(demand.Body).ReadToEndAsync();
        demand.Body.Seek(0, SeekOrigin.Begin);

        var activity = Activity.Current;
        activity?.AddTag("http.request.body", body.MaskSensitiveData());
        string headers = string.Join(", ", demand.Headers.Select(h => h.Key + "=" + h.Value).ToArray());
        activity?.AddTag("http.response.headers", headers);

        _logger.LogInformation("{Method} {Uri} {@Headers} {@Body}", demand.Method, demand.Path, headers, body.MaskSensitiveData());
    }
}