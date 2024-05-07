using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using FoundFlow.Application.Common.Behaviours;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Services;
using Microsoft.Extensions.Configuration;

namespace FoundFlow.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterServices(configuration);
        services.AddApplication();
        return services;
    }

    private static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(Application.IAssemblyEntryPoint).Assembly;

        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
    }

    private static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITokenService, TokenService>();
        services.AddSingleton<IKeyDBService>(_ =>
        {
            string connectionString = configuration.GetConnectionString("CacheDatabase");
            return new KeyDBService(connectionString);
        });
    }
}
