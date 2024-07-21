using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace FoundFlow.Application.Examples;
public class ValidationProblemDetailsExample : IExamplesProvider<ValidationProblemDetails>
{
    public ValidationProblemDetails GetExamples()
    {
        var errors = new Dictionary<string, string[]>
            {
                { "PropriedadeUm", new[] { "O campo 'PropriedadeUm' é obrigatório." } },
                { "PropriedadeDois", new[] { "O campo 'PropriedadeDois' precisa ser maior que 0." } }
            };

        return new ValidationProblemDetails(errors)
        {
            Title = "Erro de validação.",
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Status = 422,
            Instance = "https://example.com/api/v1/items"
        };
    }
}
