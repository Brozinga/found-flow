namespace FoundFlow.Shared.Extensions;

public static class Crypto
{
    private const int WorkFactor = 12;

    public static string Hash(string text)
    {
        string hashText = BCrypt.Net.BCrypt.HashPassword(text, WorkFactor);
        return hashText;
    }

    public static bool Verify(string text, string hash)
    {
        bool verify = BCrypt.Net.BCrypt.Verify(text, hash);
        return verify;
    }
}