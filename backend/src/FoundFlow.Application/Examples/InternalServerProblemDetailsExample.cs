using FoundFlow.Shared.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace FoundFlow.Application.Examples;
public class InternalServerProblemDetailsExample : IExamplesProvider<CustomProblemDetails>
{
    public CustomProblemDetails GetExamples()
    {
        return new CustomProblemDetails(new List<string> { "Problema com o servidor." })
        {
            Detail = "Problema com o servidor.",
            Status = StatusCodes.Status500InternalServerError,
            Title = "Erro interno do servidor.",
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1",
            Instance = "https://example.com/api/v1/items"
        };
    }
}
