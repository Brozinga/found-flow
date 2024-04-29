#nullable enable

using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Asp.Versioning.ApiExplorer;
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

        var swaggerSettings = config.GetValueOrThrow<SwaggerSettings>("SwaggerSettings");

        if (swaggerSettings.Enable)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider =
                    app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (string description in provider.ApiVersionDescriptions.Select(x => x.GroupName)
                             .Where(x => x != null))
                {
                    options.SwaggerEndpoint($"/swagger/{description}/swagger.json", description.ToUpperInvariant());
                }
            });
        }

        app.UseHttpsRedirection();

        app.UseStatusCodePages();

        var loggingConfig = config.GetValueOrThrow<LoggingSettings>("LoggingSettings");

        if (loggingConfig.LogRequestEnabled)
        {
            app.UseSerilogRequestLogging();
            app.UseMiddleware<LogRequestMiddleware>();
        }

        if (loggingConfig.LogResponseEnabled)
            app.UseMiddleware<LogResponseMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseWebSockets();
        app.UseRouting();
        app.UseCors(CorsPoliceName);
        app.UseHealthChecks("/health");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers().RequireCors(CorsPoliceName);

            endpoints.MapGraphQL();

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