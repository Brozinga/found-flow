using System.Diagnostics.CodeAnalysis;
using FoundFlow.Shared.Documentation.Examples;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using FoundFlow.Shared.Documentation.Responses;

namespace FoundFlow.WebApi.Controllers;

/// <summary>
/// Cria uma nova inst�ncia de `BaseController`.
/// </summary>
/// <param name="sender">O objeto `ISender` usado para enviar solicita��es MediatR.</param>
/// <param name="logger">O objeto `ILogger` usado para registrar logs.</param>
[ExcludeFromCodeCoverage]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno no servidor.", typeof(ErrorResponse))]
[SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerProblemDetailsExample))]

public abstract class BaseController(ISender sender, ILogger logger) : ControllerBase
{
    /// <summary>
    /// O objeto `ILogger` usado para registrar logs.
    /// </summary>
    protected ILogger Logger { get; } = logger ?? throw new System.ArgumentNullException(nameof(logger));

    /// <summary>
    /// O objeto `ISender` usado para enviar solicita��es MediatR.
    /// </summary>
    protected ISender Sender { get; } = sender;
}