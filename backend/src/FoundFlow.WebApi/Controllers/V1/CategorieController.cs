using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Categories.Create;
using FoundFlow.Application.Common.Feature.Categories.Delete;
using FoundFlow.Application.Common.Feature.Categories.Update;
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
    /// <param name="request">Formulário de inserção de categoria.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(CreateCategorieResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateCategorieRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    /// <summary>
    /// Rota responsável por atualizar uma categoria no banco de dados.
    /// </summary>
    /// <param name="request">Formulário de atualização de categoria.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromBody] UpdateCategorieRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status);
    }

    /// <summary>
    /// Rota responsável por deletar uma categoria no banco de dados.
    /// </summary>
    /// <param name="request">Formulário de deleção de categoria.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(DeleteCategorieResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Delete([FromBody] DeleteCategorieRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }
}