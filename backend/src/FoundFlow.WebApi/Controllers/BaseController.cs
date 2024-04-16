using System.Diagnostics.CodeAnalysis;
using FoundFlow.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoundFlow.WebApi.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[AllowAnonymous]
[ApiExceptionHandlingFilter]
[Route("api/v{version:apiVersion}")]
[Produces("application/json")]
[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public abstract class BaseController<T> : ControllerBase
    where T : BaseController<T>
{
    protected BaseController(ISender sender, ILogger<T> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    protected ILogger<T> Logger { get; }
    protected ISender Sender { get; }
}