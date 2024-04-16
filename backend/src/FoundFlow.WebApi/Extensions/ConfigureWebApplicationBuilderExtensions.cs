using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FoundFlow.Shared.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Orleans;
using Orleans.Hosting;
using Serilog;

namespace FoundFlow.WebApi.Extensions;

[SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed")]
public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
    {
        AddConfigurationFiles(builder);
        AddSerilog(builder);
        AddOpenTelemetry(builder);

        return builder;
    }

    private static void AddConfigurationFiles(WebApplicationBuilder builder)
    {
        IHostEnvironment env = builder.Environment;

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
    }

    private static void AddOpenTelemetry(WebApplicationBuilder builder)
    {
        OpenTelemetrySettings openTelemetrySettings = builder.Configuration.GetValueOrThrow<OpenTelemetrySettings>("OpenTelemetrySettings");

        Action<ResourceBuilder> configureResource = r => r.AddService(
            serviceName: openTelemetrySettings.Application!,
            serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
            serviceInstanceId: Environment.MachineName);

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(openTelemetrySettings.Application!);
            })
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(openTelemetrySettings.Endpoint!);
                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                    o.Headers = $"authorization={openTelemetrySettings.Authorization}";
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(openTelemetrySettings.Endpoint!);
                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                    o.Headers = $"authorization={openTelemetrySettings.Authorization}";
                }));

        builder.Logging.AddOpenTelemetry(options =>
        {
            ResourceBuilder resourceBuilder = ResourceBuilder.CreateDefault();
            configureResource(resourceBuilder);
            options.SetResourceBuilder(resourceBuilder);
        });
    }

    private static void AddSerilog(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog(
            (hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            });
    }

    private static void AddOrleansDasbhard(WebApplicationBuilder builder)
    {
        builder.Host.UseOrleans(siloBuilder =>
        {
            siloBuilder.UseLocalhostClustering();
            siloBuilder.UseDashboard(x => x.HostSelf = true);
        });
    }
}