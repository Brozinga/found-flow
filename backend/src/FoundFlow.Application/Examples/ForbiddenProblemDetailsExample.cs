using FoundFlow.Shared.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace FoundFlow.Application.Examples;
public class ForbiddenProblemDetailsExample : IExamplesProvider<CustomProblemDetails>
{
    public CustomProblemDetails GetExamples()
    {
        return new CustomProblemDetails(new List<string> { "Você não tem autorização." })
        {
            Detail = "Você não tem autorização.",
            Status = StatusCodes.Status403Forbidden,
            Title = "Acesso proibido.",
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.3",
            Instance = "https://example.com/api/v1/items"
        };
    }
}
