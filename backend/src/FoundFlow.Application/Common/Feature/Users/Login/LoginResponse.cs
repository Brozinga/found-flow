using System;

namespace FoundFlow.Application.Common.Feature.Users.Login;

/// <summary>
/// Representa a resposta da solicitação de login, contendo informações sobre o token de acesso e sua validade.
/// </summary>
/// <remarks>
/// Cria uma nova instância de `LoginResponse`.
/// </remarks>
/// <param name="token">O token de acesso JWT.</param>
/// <param name="expires">A data e hora de expiração do token (opcional).</param>
/// <param name="succeeded">Indica se o login foi bem-sucedido (padrão: true).</param>
public sealed class LoginResponse(string token, DateTime? expires, bool succeeded = true)
{
    /// <summary>
    /// O token de acesso JWT.
    /// </summary>
    /// <example>eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE3MjE1MjIwNjcsImVtYWlsIjoiYW50b25pby5zaWx2YUBlbWFpbC5jb20ifQ.ZyDXex0UQ_iKQOmOfw5yS2p90XftY6Vts9CmcRa8S-o</example>
    public string Token { get; } = token;

    /// <summary>
    /// A data e hora de expiração do token em milissegundos com o formato <a href="https://developer.mozilla.org/en-US/docs/Glossary/Unix_time">(Unix)</a>.
    /// Se o token não tiver data de expiração, o valor será 0.
    /// </summary>
    /// <example>1677628800000</example>
    public long Expires { get; private set; } = SetExpires(expires);

    /// <summary>
    /// Indica se o login foi bem-sucedido.
    /// </summary>
    /// <example>true</example>
    public bool Succeeded { get; } = succeeded;

    /// <summary>
    /// Converte a data de expiração (DateTime?) em milissegundos desde a época Unix.
    /// </summary>
    /// <param name="expireDate">A data de expiração (opcional).</param>
    /// <returns>O tempo em milissegundos desde a época Unix ou 0 se a data de expiração for nula.</returns>
    private static long SetExpires(DateTime? expireDate)
    {
        if (expireDate == null)
            return 0;

        var dt = new DateTimeOffset(expireDate.Value);
        return dt.ToUnixTimeMilliseconds();
    }
}