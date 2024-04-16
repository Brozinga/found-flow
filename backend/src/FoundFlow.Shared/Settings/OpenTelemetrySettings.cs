using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class OpenTelemetrySettings : IAppSettings
{
    public string? Endpoint { get; init; }
    public string? Application { get; init; }
    public string? Authorization { get; init; }
}