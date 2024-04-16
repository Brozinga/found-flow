using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using FoundFlow.Application.DependencyInjection;
using FoundFlow.Infrastructure.DependencyInjection;
using FoundFlow.Shared.Settings;
using FoundFlow.WebApi.Extensions;
using Orleans;
using Sentry;
using Serilog.Sinks.OpenTelemetry;

namespace FoundFlow.WebApi;

[ExcludeFromCodeCoverage]
public class Program
{
    protected Program()
    {
    }

    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.OpenTelemetry()
            .CreateLogger();

        Log.Information("Starting up");

        try
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Configure();
            builder.Services.Configure();
            builder.Services.AddInfrastructure(builder.Configuration);

            WebApplication app = builder.Build();
            app.UseInfrastructure(builder.Configuration);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.Information("Server shutting down...");
            Log.CloseAndFlush();
        }
    }
}