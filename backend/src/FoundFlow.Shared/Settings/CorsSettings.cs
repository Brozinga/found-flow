using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class CorsSettings : IAppSettings
{
    public IReadOnlyCollection<string> Origin { get; init; } = new List<string>();
}