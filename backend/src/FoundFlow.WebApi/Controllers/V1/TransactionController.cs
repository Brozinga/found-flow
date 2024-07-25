using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Transactions.Create;
using FoundFlow.Application.Common.Feature.Transactions.Delete;
using FoundFlow.Application.Common.Feature.Transactions.Update;
using FoundFlow.Shared.Documentation.Examples;
using FoundFlow.Shared.Documentation.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FoundFlow.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Tags("Transação")]
public class TransactionController(ISender sender, ILogger<TransactionController> logger) : BaseController(sender, logger)
{
    [Authorize]
    [HttpPost]
    [SwaggerOperation("Adicionar", "Endpoint responsável por criar uma transação.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Transação criada com sucesso.", typeof(CreateTransactionResponse), Description = "Um objeto `CreateTransactionResponse` indicando se a transação foi criada com sucesso e seu ID.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida ou transação já existe.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Transação não encontrada.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "A requisição não foi bem-sucedida porque falta autenticação válida.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "O cliente não tem permissão para acessar o recurso solicitado.", typeof(ErrorResponse))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(ForbiddenProblemDetailsExample))]
    public async Task<IActionResult> Create(
    [FromBody][SwaggerParameter("Representa uma solicitação para adicionar uma nova transação.", Required = true)]
    CreateTransactionRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    [Authorize]
    [HttpPut]
    [SwaggerOperation("Atualizar", "Endpoint responsável por atualizar uma transação.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Transação atualizada com sucesso.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Transação inválida.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Transação não encontrada.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "A requisição não foi bem-sucedida porque falta autenticação válida.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "O cliente não tem permissão para acessar o recurso solicitado.", typeof(ErrorResponse))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(ForbiddenProblemDetailsExample))]
    public async Task<IActionResult> Update(
    [FromBody][SwaggerParameter("Representa uma solicitação para atualizar uma transação existente.", Required = true)]
    UpdateTransactionRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status);
    }

    [Authorize]
    [HttpDelete]
    [SwaggerOperation("Deletar", "Endpoint responsável por deletar uma transação.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Transação deletada com sucesso.", typeof(DeleteTransactionResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Transação inválida.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Transação não encontrada.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "A requisição não foi bem-sucedida porque falta autenticação válida.", typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "O cliente não tem permissão para acessar o recurso solicitado.", typeof(ErrorResponse))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(ForbiddenProblemDetailsExample))]
    public async Task<IActionResult> Delete(
    [FromBody][SwaggerParameter("Representa uma solicitação para excluir uma transação existente.", Required = true)]
    DeleteTransactionRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }
}