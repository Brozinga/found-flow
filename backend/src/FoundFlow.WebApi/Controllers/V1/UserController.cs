using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Users.Create;
using FoundFlow.Application.Common.Feature.Users.Login;
using FoundFlow.Application.Common.Feature.Users.ResetPassword;
using FoundFlow.Application.Common.Feature.Users.Update;
using FoundFlow.Shared.ProblemDetails;
using FoundFlow.Application.Examples;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace FoundFlow.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Tags("Usuários")]
public class UserController(ISender sender, ILogger<UserController> logger) : BaseController(sender, logger)
{
    [HttpPost("login")]
    [SwaggerOperation("Login", "Endpoint responsável por solicitar um token JWT para acesso as rotas internas.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Login realizado com sucesso.", typeof(LoginResponse), Description = "Um objeto `LoginResponse` contendo o token de acesso e informações sobre sua validade.")]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    public async Task<IActionResult> GetToken(
        [FromBody][SwaggerParameter("Formulário para autenticação do usuário.", Required = true)]
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    [HttpPost("register")]
    [SwaggerOperation("Adicionar", "Endpoint responsável para redefinir a senha de um usuário.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Usuário criado com sucesso.", typeof(CreateUserResponse), Description = "Um objeto `CreateUserResponse` indicando se o usuário foi criado com sucesso e seu ID.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida ou usuário já existente.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerProblemDetailsExample))]
    public async Task<IActionResult> Register(
        [FromBody][SwaggerParameter("Formulário de criação de usuário.", Required = true)]
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    [HttpPost("reset-password")]
    [SwaggerOperation("Redefinir a senha", "Endpoint responsável para redefinir a senha de um usuário.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Solicitação de redefinição de senha processada com sucesso.", typeof(ResetPasswordResponse), Description = "Representa a resposta, contendo a nova senha gerada.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]

    public async Task<IActionResult> ResetPassword(
        [FromBody][SwaggerParameter("Formulário de reset de senha.", Required = true)]
        ResetPasswordRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    [Authorize]
    [HttpPut]
    [SwaggerOperation("Atualizar", "Endpoint responsável por atualizar um usuário.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Usuário atualizado com sucesso.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Usuário não enviado corretamente.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    public async Task<IActionResult> Update(
        [FromBody][SwaggerParameter("Formulário de atualização de usuário.", Required = true)]
        UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status);
    }
}