using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class DocumentationSettings : IAppSettings
{
    public bool Enable { get;  init; }
    public string? ContactName { get;  init; }
    public string? ContactUrl { get;  init; }
    public string? ApiName { get;  init; }
    public string? ApiDescription { get;  init; }
}