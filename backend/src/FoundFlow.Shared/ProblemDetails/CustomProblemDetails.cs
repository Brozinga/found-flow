using System.Diagnostics.CodeAnalysis;

namespace FoundFlow.Shared.ProblemDetails;

/// <summary>
/// Representa detalhes de um problema ocorrido em uma solicitação de API, incluindo informações adicionais sobre erros.
/// </summary>
[ExcludeFromCodeCoverage]
public class CustomProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    private readonly Dictionary<string, string[]> _errors = new();

    /// <summary>
    /// Cria uma nova instância de `CustomProblemDetails` com uma lista de erros gerais.
    /// </summary>
    /// <param name="errors">Uma coleção de strings representando as mensagens de erro gerais.</param>
    public CustomProblemDetails(IEnumerable<string> errors)
    {
        if (errors is null) return;

        foreach (string error in errors)
        {
            AddError("General", new[] { error });
        }
    }

    /// <summary>
    /// Obtém um dicionário somente leitura contendo os erros agrupados por chave, onde cada chave representa um tipo de erro e os valores são as mensagens de erro correspondentes.
    /// </summary>
    /// <example>
    /// {
    ///     "General": [ "Erro ao processar a solicitação." ],
    ///     "Nome": [ "O nome é obrigatório." ]
    /// }
    /// </example>
    public IReadOnlyDictionary<string, string[]> Errors => _errors;

    /// <summary>
    /// Adiciona uma ou mais mensagens de erro a uma chave específica no dicionário de erros.
    /// Se a chave já existir, as novas mensagens são adicionadas à lista existente.
    /// </summary>
    /// <param name="key">A chave que identifica o tipo de erro.</param>
    /// <param name="errorMessages">Uma coleção de strings contendo as mensagens de erro a serem adicionadas.</param>
    public void AddError(string key, string[] errorMessages)
    {
        if (errorMessages is null) return;

        if (!_errors.ContainsKey(key))
        {
            _errors[key] = errorMessages;
        }
        else
        {
            string[] existingErrors = _errors[key];
            string[] newErrors = new string[existingErrors.Length + errorMessages.Length];

            existingErrors.CopyTo(newErrors, 0);
            errorMessages.CopyTo(newErrors, existingErrors.Length);

            _errors[key] = newErrors;
        }
    }
}
