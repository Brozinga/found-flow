using System;

namespace FoundFlow.Application.Common.Feature.Users.Login;

public sealed class LoginResponse
{
    public LoginResponse(string token, DateTime? expires, bool succeeded = true)
    {
        Token = token;
        Succeeded = succeeded;
        Expires = SetExpires(expires);
    }

    public string Token { get; }
    public long Expires { get; private set; }
    public bool Succeeded { get; }
    private long SetExpires(DateTime? expireDate)
    {
        if (expireDate == null)
            return 0;

        var dt = new DateTimeOffset(expireDate.Value);
        return dt.ToUnixTimeMilliseconds();

    }
}