using FoundFlow.Shared.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace FoundFlow.Application.Examples;
public class BadRequestProblemDetailsExample : IExamplesProvider<CustomProblemDetails>
{
    public CustomProblemDetails GetExamples()
    {
        return new CustomProblemDetails(new List<string> { "Um problema na requisição encontrada." })
        {
            Detail = "Um problema na requisição encontrada.",
            Status = StatusCodes.Status400BadRequest,
            Title = "Requisição mal formatada.",
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Instance = "https://example.com/api/v1/items"
        };
    }
}
