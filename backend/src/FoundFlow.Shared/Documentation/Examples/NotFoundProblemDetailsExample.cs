using FoundFlow.Shared.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace FoundFlow.Shared.Documentation.Examples;
public class NotFoundProblemDetailsExample : IExamplesProvider<CustomProblemDetails>
{
    public CustomProblemDetails GetExamples()
    {
        return new CustomProblemDetails(new List<string> { "O resultado não foi encontrado." })
        {
            Detail = "O resultado não foi encontrado.",
            Status = StatusCodes.Status404NotFound,
            Title = "Recurso não encontrado.",
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
            Instance = "https://example.com/api/v1/items"
        };
    }
}
