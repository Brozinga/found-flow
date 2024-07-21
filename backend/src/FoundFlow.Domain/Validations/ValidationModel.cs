using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoundFlow.Domain.Validations;

/// <summary>
/// Representa o resultado da validação de uma entidade ou operação.
/// </summary>
public class ValidationModel
{
    /// <summary>
    /// Cria um novo objeto ValidationModel com os valores especificados.
    /// </summary>
    /// <param name="isValid">Indica se a validação foi bem-sucedida.</param>
    /// <param name="validationResults">Lista de resultados da validação.</param>
    /// <param name="errorMessages">Lista de mensagens de erro.</param>
    public ValidationModel(bool isValid, List<ValidationResult> validationResults, List<string> errorMessages)
    {
        IsValid = isValid;
        ValidationResults = validationResults.AsReadOnly();
        ErrorMessages = errorMessages.AsReadOnly();
    }

    /// <summary>
    /// Cria um novo objeto ValidationModel com os valores especificados.
    /// </summary>
    /// <param name="isValid">Indica se a validação foi bem-sucedida.</param>
    /// <param name="validationResults">Lista de resultados da validação.</param>
    public ValidationModel(bool isValid, List<ValidationResult> validationResults)
    {
        IsValid = isValid;
        ValidationResults = validationResults.AsReadOnly();
        ErrorMessages = validationResults.ConvertAll(result => result.ErrorMessage);
    }

    /// <summary>
    /// Cria um novo objeto ValidationModel indicando falha na validação.
    /// </summary>
    /// <param name="validationResults">Lista de resultados da validação.</param>
    public ValidationModel(List<ValidationResult> validationResults)
    {
        IsValid = false;
        ValidationResults = validationResults.AsReadOnly();
        ErrorMessages = validationResults.ConvertAll(result => result.ErrorMessage);
    }

    /// <summary>
    /// Indica se a validação foi bem-sucedida.
    /// </summary>
    public bool IsValid { get; private set; }

    /// <summary>
    /// Coleção de resultados da validação.
    /// </summary>
    public IReadOnlyCollection<ValidationResult> ValidationResults { get; private set; }

    /// <summary>
    /// Coleção de mensagens de erro.
    /// </summary>
    public IReadOnlyCollection<string> ErrorMessages { get; private set; }
}