using System.Diagnostics.CodeAnalysis;
using FoundFlow.Application.Examples;
using FoundFlow.Shared.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FoundFlow.WebApi.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor.", typeof(CustomProblemDetails))]
[SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerProblemDetailsExample))]

public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Cria uma nova instância de `BaseController`.
    /// </summary>
    /// <param name="sender">O objeto `ISender` usado para enviar solicitações MediatR.</param>
    /// <param name="logger">O objeto `ILogger` usado para registrar logs.</param>
    protected BaseController(ISender sender, ILogger logger)
    {
        Sender = sender;
        Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// O objeto `ILogger` usado para registrar logs.
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// O objeto `ISender` usado para enviar solicitações MediatR.
    /// </summary>
    protected ISender Sender { get; }
}