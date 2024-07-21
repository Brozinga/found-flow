namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;

/// <summary>
/// Representa a resposta da solicitação de redefinição de senha, contendo a nova senha gerada.
/// </summary>
public class ResetPasswordResponse
{
    /// <summary>
    /// Cria uma nova instância de `ResetPasswordResponse`.
    /// </summary>
    /// <param name="newPassword">A nova senha gerada para o usuário.</param>
    public ResetPasswordResponse(string newPassword)
    {
        NewPassword = newPassword;
    }

    /// <summary>
    /// A nova senha gerada para o usuário.
    /// </summary>
    /// <example>NovaSenha123!</example>
    public string NewPassword { get; private set; }
}