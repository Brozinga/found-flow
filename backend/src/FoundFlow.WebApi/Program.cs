using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using FoundFlow.Application.DependencyInjection;
using FoundFlow.Infrastructure.DependencyInjection;
using FoundFlow.WebApi.Extensions;

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
            .CreateLogger();

        Log.Information("Starting up");

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configure();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.Configure(builder.Configuration);

            var app = builder.Build();

            app.UseInfrastructure(builder.Configuration, builder.Environment);

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