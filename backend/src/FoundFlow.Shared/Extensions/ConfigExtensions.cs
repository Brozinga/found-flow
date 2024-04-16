using FoundFlow.Shared.Interfaces;

namespace Microsoft.Extensions.Configuration;

public static class ConfigExtensions
{
    public static T GetValueOrThrow<T>(this IConfiguration config, string configKey)
    {
      T? configuration = config.GetSection(configKey).Get<T>();

      ArgumentNullException.ThrowIfNull(configuration);

      return configuration;
    }
}