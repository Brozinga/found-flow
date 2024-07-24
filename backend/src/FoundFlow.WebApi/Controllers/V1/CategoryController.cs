using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Categories.Create;
using FoundFlow.Application.Common.Feature.Categories.Delete;
using FoundFlow.Application.Common.Feature.Categories.Update;
using FoundFlow.Application.Examples;
using FoundFlow.Shared.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FoundFlow.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Tags("Categoria")]
public class CategoryController(ISender sender, ILogger<CategoryController> logger) : BaseController(sender, logger)
{
    [Authorize]
    [HttpPost]
    [SwaggerOperation("Adicionar", "Endpoint responsável por criar uma categoria.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Categoria criado com sucesso.", typeof(CreateCategorieResponse), Description = "Um objeto `CreateUserResponse` indicando se o usuário foi criado com sucesso e seu ID.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida ou categoria já existente.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria não encontrado.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "A requisição não foi bem-sucedida porque falta autenticação válida.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "O cliente não tem permissão para acessar o recurso solicitado.", typeof(CustomProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(ForbiddenProblemDetailsExample))]
    public async Task<IActionResult> Create(
        [FromBody][SwaggerParameter("Representa uma solicitação para adicionar uma nova categoria.", Required = true)]
        CreateCategorieRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    [Authorize]
    [HttpPut]
    [SwaggerOperation("Atualizar", "Endpoint responsável por atualizar uma categoria.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Categoria atualizada com sucesso.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria não encontrada.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "A requisição não foi bem-sucedida porque falta autenticação válida.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "O cliente não tem permissão para acessar o recurso solicitado.", typeof(CustomProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(ForbiddenProblemDetailsExample))]
    public async Task<IActionResult> Update(
    [FromBody][SwaggerParameter("Representa uma solicitação para atualizar uma categoria existente.", Required = true)]
    UpdateCategorieRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status);
    }

    [Authorize]
    [HttpDelete]
    [SwaggerOperation("Deletar", "Endpoint responsável por deletar uma categoria.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Categoria deletada com sucesso.", typeof(DeleteCategorieResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Categoria não encontrada.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "A requisição não foi bem-sucedida porque falta autenticação válida.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status403Forbidden, "O cliente não tem permissão para acessar o recurso solicitado.", typeof(CustomProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(ForbiddenProblemDetailsExample))]
    public async Task<IActionResult> Delete(
    [FromBody][SwaggerParameter("Representa uma solicitação para deletar uma categoria existente.", Required = true)]
    DeleteCategorieRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

}