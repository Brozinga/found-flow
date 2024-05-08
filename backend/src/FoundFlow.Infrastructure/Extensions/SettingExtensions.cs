using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using FoundFlow.Shared.Settings;

namespace FoundFlow.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SettingExtensions
{
    internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
    {
        return services
            .Configure<IpRateLimitSettings>(config.GetSection(nameof(IpRateLimitSettings)))
            .Configure<LoggingSettings>(config.GetSection(nameof(LoggingSettings)))
            .Configure<JwtSettings>(config.GetSection(nameof(JwtSettings)))
            .Configure<MongoDBSettings>(config.GetSection(nameof(MongoDBSettings)))
            .Configure<CorsSettings>(config.GetSection(nameof(CorsSettings)))
            .Configure<SwaggerSettings>(config.GetSection(nameof(SwaggerSettings)));
    }
}