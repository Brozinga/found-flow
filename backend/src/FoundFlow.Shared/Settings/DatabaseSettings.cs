using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class DatabaseSettings : IAppSettings
{
    public string? DBProvider { get; init; }
    public string? ConnectionString { get; init; }
}