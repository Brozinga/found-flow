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
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SharpCompress.Common;
using System.IO;

namespace FoundFlow.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration config, IWebHostEnvironment env)
    {
        const string CorsPoliceName = "CorsPolicy";

        if (config.GetValue<bool>("IpRateLimitSettings:EnableEndpointRateLimiting"))
            app.UseIpRateLimiting();

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseProblemDetails();

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

            app.UseReDoc(c =>
            {
                c.DocumentTitle = "Found Flow API - Documentação";
                c.SpecUrl = "/swagger/v1/swagger.json";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "redoc.html");
                c.IndexStream = () => new FileStream(filePath, FileMode.Open, FileAccess.Read);
            });
        }

        app.UseHttpsRedirection();

        var loggingConfig = config.GetValueOrThrow<LoggingSettings>("LoggingSettings");

        if (loggingConfig.LogRequestEnabled)
        {
            app.UseSerilogRequestLogging();
            app.UseMiddleware<LogRequestMiddleware>();
        }

        if (loggingConfig.LogResponseEnabled)
            app.UseMiddleware<LogResponseMiddleware>();

        app.UseStaticFiles();
        app.UseWebSockets();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

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