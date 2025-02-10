using System.Text.RegularExpressions;

namespace FoundFlow.Frontend.Pages.Helpers;

public abstract partial class Validations
{
    private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    private const string ColorPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";

    [GeneratedRegex(EmailPattern)]
    private static partial Regex MyEmailRegex();
    
    [GeneratedRegex(ColorPattern)]
    private static partial Regex MyColorRegex();
    
    public static string EmailIsValid(string email, string message)
    {
        return MyEmailRegex().IsMatch(email) ? string.Empty : message;
    }
    
    public static string ColorValid(string color, string message)
    {
        return MyColorRegex().IsMatch(color) ? string.Empty : message;
    }
    
    public static string TextIsNullOrEmpty(string text, string message)
    {
        return !string.IsNullOrEmpty(text) ? string.Empty : message;
    }
    
    public static string MinLength(string text, int minLength, string message)
    {
        return text.Length > minLength ? string.Empty : message;
    }
    
    public static string MaxLength(string text, int maxLength, string message)
    {
        return text.Length <= maxLength ? string.Empty : message;
    }

    public static string MinOrEqualsLength(string text, int minLength, string message)
    {
        return text.Length >= minLength ? string.Empty : message;
    }

    public static string MaxOrEqualsLength(string text, int maxLength, string message)
    {
        return text.Length <= maxLength ? string.Empty : message;
    }

    public static string EqualsLength(string text, int equalsLength, string message)
    {
        return text.Length == equalsLength ? string.Empty : message;
    }

    public static string BetweenLength(string text, int minLength, int maxLength, string message)
    {
        return text.Length >= minLength && text.Length <= maxLength ? string.Empty : message;
    }

    public static string Equals(string primaryText, string secondaryText, string message)
    {
        return primaryText == secondaryText ? string.Empty : message;
    }
    
    public static string ValidatePassword(string password, string message)
    {
        // Verifica se a senha contém pelo menos uma letra maiúscula, uma letra minúscula, um número
        // e se tem pelo menos 6 caracteres
        var hasUpperCase = password.Any(char.IsUpper);
        var hasLowerCase = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);
        var hasMinimumLength = password.Length >= 6;

        if (hasUpperCase && hasLowerCase && hasDigit && hasMinimumLength)
        {
            return string.Empty; // Senha válida
        }

        return message; // Senha inválida
    }

    public static bool VerifyIfNoExistErrors(params string[] errors)
    {
        return new List<string>(errors).TrueForAll(string.IsNullOrEmpty);
    }
}