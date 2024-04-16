using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoundFlow.Domain.Validations;

public class ValidationModel
{
    public ValidationModel(bool isValid, List<ValidationResult> validationResults, List<string> errorMessages)
    {
        IsValid = isValid;
        ValidationResults = validationResults.AsReadOnly();
        ErrorMessages = errorMessages.AsReadOnly();
    }

    public ValidationModel(bool isValid, List<ValidationResult> validationResults)
    {
        IsValid = isValid;
        ValidationResults = validationResults.AsReadOnly();
        ErrorMessages = validationResults.ConvertAll(result => result.ErrorMessage);
    }

    public ValidationModel(List<ValidationResult> validationResults)
    {
        IsValid = false;
        ValidationResults = validationResults.AsReadOnly();
        ErrorMessages = validationResults.ConvertAll(result => result.ErrorMessage);
    }

    public bool IsValid { get; private set; }
    public IReadOnlyCollection<ValidationResult> ValidationResults { get; private set; }
    public IReadOnlyCollection<string> ErrorMessages { get; private set; }
}