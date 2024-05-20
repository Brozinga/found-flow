using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Categories.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoundFlow.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Route("categorie")]
public class CategorieController : BaseController<CategorieController>
{
    public CategorieController(ISender sender, ILogger<CategorieController> logger)
        : base(sender, logger)
    {
    }

    /// <summary>
    /// Rota responsável por criar uma categoria no banco de dados.
    /// </summary>
    /// <param name="request">Formulário de categoria.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpPost("add")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(CreateCategorieResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Insert([FromBody] CreateCategorieRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }
}