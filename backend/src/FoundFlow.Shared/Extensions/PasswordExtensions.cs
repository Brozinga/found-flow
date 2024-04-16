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
}