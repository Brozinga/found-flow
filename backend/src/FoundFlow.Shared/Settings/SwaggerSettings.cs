using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class SwaggerSettings : IAppSettings
{
    public bool Enable { get;  init; }
    public string? ContactName { get;  init; }
    public Uri? ContactUrl { get;  init; }
    public string? ApiName { get;  init; }
    public string? ApiDescription { get;  init; }
}