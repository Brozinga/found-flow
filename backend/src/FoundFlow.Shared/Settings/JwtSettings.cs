using FoundFlow.Shared.Interfaces;

namespace FoundFlow.Shared.Settings;

public class JwtSettings : IAppSettings
{
    public required string Key { get; init; }
    public int TokenExpirationInMinutes { get; init; }
    public int RefreshTokenExpirationInDays { get; init; }
}