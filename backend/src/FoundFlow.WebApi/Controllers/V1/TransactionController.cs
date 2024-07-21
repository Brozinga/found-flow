using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Transactions.Create;
using FoundFlow.Application.Common.Feature.Transactions.Delete;
using FoundFlow.Application.Common.Feature.Transactions.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoundFlow.WebApi.Controllers.V1;

[ApiVersion("1.0")]
public class TransactionController : BaseController
{
    public TransactionController(ISender sender, ILogger<TransactionController> logger)
        : base(sender, logger)
    {
    }

    /// <summary>
    /// Rota responsável por criar uma transação no banco de dados.
    /// </summary>
    /// <param name="request">Formulário de inserção de transação.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(CreateTransactionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    /// <summary>
    /// Rota responsável por atualizar uma transação no banco de dados.
    /// </summary>
    /// <param name="request">Formulário de atualização de transação.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromBody] UpdateTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status);
    }

    /// <summary>
    /// Rota responsável por deletar uma transação no banco de dados.
    /// </summary>
    /// <param name="request">Formulário de deleção de transação.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(DeleteTransactionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Delete([FromBody] DeleteTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }
}