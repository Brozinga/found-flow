using System.Diagnostics.CodeAnalysis;
using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

[ExcludeFromCodeCoverage]
public class LoggingSettings : IAppSettings
{
    public bool LogRequestEnabled { get; set; }
    public bool LogResponseEnabled { get; set; }
}