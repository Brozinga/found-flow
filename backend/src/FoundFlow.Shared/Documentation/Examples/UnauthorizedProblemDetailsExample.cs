using FoundFlow.Shared.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace FoundFlow.Shared.Documentation.Examples;
public class UnauthorizedProblemDetailsExample : IExamplesProvider<CustomProblemDetails>
{
    public CustomProblemDetails GetExamples()
    {
        return new CustomProblemDetails(new List<string> { "A autenticação não é válida." })
        {
            Detail = "A autenticação não é válida.",
            Status = StatusCodes.Status401Unauthorized,
            Title = "Acesso não autorizado.",
            Type = "https://www.rfc-editor.org/rfc/rfc7235#section-3.1",
            Instance = "https://example.com/api/v1/items"
        };
    }
}
