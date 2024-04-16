#nullable enable

using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FoundFlow.Infrastructure.Middleware;
using FoundFlow.Shared.Settings;

namespace FoundFlow.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration config)
    {
        const string CorsPoliceName = "CorsPolicy";

        if (config.GetValue<bool>("IpRateLimitSettings:EnableEndpointRateLimiting"))
            app.UseIpRateLimiting();

        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();

        SwaggerSettings swaggerSettings = config.GetValueOrThrow<SwaggerSettings>("SwaggerSettings");

        if (swaggerSettings.Enable)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                IApiVersionDescriptionProvider provider =
                    app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (string description in provider.ApiVersionDescriptions.Select(x => x.GroupName)
                             .Where(x => x != null))
                {
                    options.SwaggerEndpoint($"/swagger/{description}/swagger.json", description.ToUpperInvariant());
                }
            });
        }

        app.UseSerilogRequestLogging();

        LoggingSettings loggingConfig = config.GetValueOrThrow<LoggingSettings>("LoggingSettings");

        if (loggingConfig.LogRequestEnabled)
            app.UseMiddleware<LogRequestMiddleware>();

        if (loggingConfig.LogResponseEnabled)
            app.UseMiddleware<LogResponseMiddleware>();

        app.UseRouting();
        app.UseCors(CorsPoliceName);
        app.UseAuthorization();
        app.UseHealthChecks("/health");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers().RequireCors(CorsPoliceName);
            endpoints.MapHealthChecks("/ready", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("ready"),
            });
            endpoints.MapHealthChecks("/live", new HealthCheckOptions()
            {
                Predicate = (_) => false
            });
        });

        return app;
    }
}