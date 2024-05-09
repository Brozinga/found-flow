using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class CorsSettings : IAppSettings
{
    public IList<string> Origin { get; init; } = new List<string>();
}