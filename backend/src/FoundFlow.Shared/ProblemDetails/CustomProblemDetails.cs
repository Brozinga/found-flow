﻿using System.Diagnostics.CodeAnalysis;

namespace FoundFlow.Shared.ProblemDetails;

[ExcludeFromCodeCoverage]
public class CustomProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    private readonly Dictionary<string, string[]> _errors = new();

    public CustomProblemDetails(IEnumerable<string> errors)
    {
        if (errors is null) return;

        foreach (string error in errors)
        {
            AddError("General", new[] { error });
        }
    }

    public IReadOnlyDictionary<string, string[]> Errors => _errors;

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
