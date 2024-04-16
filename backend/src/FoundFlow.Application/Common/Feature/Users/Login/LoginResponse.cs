using System;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public sealed class LoginResponse
{
    public LoginResponse(string token, DateTime? expires)
    {
        Token = token;
        Expires = SetExpires(expires);
    }

    public string Token { get; set; }
    public long Expires { get; private set; }

    private long SetExpires(DateTime? expireDate)
    {
        DateTimeOffset dt = new DateTimeOffset(expireDate.Value);
        return dt.ToUnixTimeMilliseconds();
    }
}