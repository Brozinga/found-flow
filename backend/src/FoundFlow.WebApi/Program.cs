using Serilog;
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

    private static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .CreateLogger();

        Log.Information("Starting up");

        try
        {
            var builder = WebApplication.CreateBuilder();
            builder.Configure();
            builder.AddServiceDefaults();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.Configure();

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