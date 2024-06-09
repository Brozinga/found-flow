using System.Text.RegularExpressions;

namespace System;

public static class PasswordExtensions
{
    public static string MaskSensitiveData(this string text)
    {
        string[] keywords = new[]
        {
            "senha",
            "password",
            "pass",
            "token"
        };
        foreach (string keyword in keywords)
        {
            string pattern = string.Format(@"(""{0}""\s*:\s*"")([^""]+)", keyword);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            text = regex.Replace(text, m => m.Groups[1].Value + new string('*', m.Groups[2].Value.Length));
        }

        return text;
    }

    public static bool ValidatePasswordComplexity(string password)
    {
        return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");
    }

    public static string GenerateRandomPassword(int length = 6)
    {
        var random = new Random();

        const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string allChars = upperCase + lowerCase + digits;

        char[] password = new char[length];
        password[0] = upperCase[random.Next(upperCase.Length)];
        password[1] = lowerCase[random.Next(lowerCase.Length)];
        password[2] = digits[random.Next(digits.Length)];

        for (int i = 3; i < length; i++)
            password[i] = allChars[random.Next(allChars.Length)];

        return new string(password.OrderBy(x => random.Next()).ToArray());
    }
}