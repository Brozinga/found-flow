using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FoundFlow.Application.Common.Behaviours;
using FoundFlow.Application.Interfaces;
using FoundFlow.Application.Services;

namespace FoundFlow.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services)
    {
        services.RegisterServices();
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
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();
    }
}
