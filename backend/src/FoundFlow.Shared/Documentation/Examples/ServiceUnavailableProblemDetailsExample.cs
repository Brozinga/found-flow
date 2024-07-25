using FoundFlow.Shared.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace FoundFlow.Shared.Documentation.Examples;
public class ServiceUnavailableProblemDetailsExample : IExamplesProvider<CustomProblemDetails>
{
    public CustomProblemDetails GetExamples()
    {
        return new CustomProblemDetails(new List<string> { "Serviço indisponível." })
        {
            Detail = "Serviço indisponível.",
            Status = StatusCodes.Status503ServiceUnavailable,
            Title = "Serviço Temporariamente Indisponível.",
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.4",
            Instance = "https://example.com/api/v1/items"
        };
    }
}
