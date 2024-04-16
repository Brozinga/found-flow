using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class IpRateLimitSettings : IAppSettings
{
    public bool EnableEndpointRateLimiting { get; init; }
    public bool StackBlockedRequests { get; init; }
}
