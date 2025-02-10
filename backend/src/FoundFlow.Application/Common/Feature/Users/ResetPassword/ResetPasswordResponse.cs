namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;

/// <summary>
/// Cria uma nova instância de `ResetPasswordResponse`.
/// </summary>
/// <param name="newPassword">A nova senha gerada para o usuário.</param>
public class ResetPasswordResponse(string newPassword)
{
    /// <summary>
    /// A nova senha gerada para o usuário.
    /// </summary>
    /// <example>NovaSenha123!</example>
    public string NewPassword { get; private set; } = newPassword;
}