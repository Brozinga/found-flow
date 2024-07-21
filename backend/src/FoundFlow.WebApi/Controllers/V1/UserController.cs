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
[SwaggerTag("Controlador responsável pelo gerenciamento de usuários da API." +
    "Este controlador lida com operações relacionadas ao login de usuários, " +
    "registro de novos usuários, redefinição de senhas e atualização das informações do usuário.")]
public class UserController : BaseController
{
    public UserController(ISender sender, ILogger<UserController> logger)
        : base(sender, logger)
    {
    }

    /// <summary>
    /// Realiza o login do usuário e retorna um token de acesso (Bearer Token).
    /// </summary>
    /// <param name="request">Os dados para autenticação do usuário.</param>
    /// <param name="cancellationToken">Token para cancelar a operação.</param>
    /// <returns>Um objeto `LoginResponse` contendo o token de acesso e informações sobre sua validade.</returns>
    [HttpPost("login")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerResponse(StatusCodes.Status200OK, "Login realizado com sucesso.", typeof(LoginResponse))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerProblemDetailsExample))]
    public async Task<IActionResult> GetToken([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    /// <summary>
    /// Registra um novo usuário no sistema.
    /// </summary>
    /// <param name="request">Os dados para cadastro do novo usuário.</param>
    /// <param name="cancellationToken">Token para cancelar a operação.</param>
    /// <returns>Um objeto `CreateUserResponse` indicando se o usuário foi criado com sucesso e seu ID.</returns>
    [HttpPost("register")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerResponse(StatusCodes.Status201Created, "Usuário criado com sucesso.", typeof(CreateUserResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Requisição inválida ou usuário já existente.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerProblemDetailsExample))]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    /// <summary>
    /// Rota responsável por resetar a senha de um usuário.
    /// </summary>
    /// <param name="request">Formulário de reset de senha.</param>
    /// <param name="cancellationToken"></param>
    [HttpPost("reset-password")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerResponse(StatusCodes.Status200OK, "Solicitação de redefinição de senha processada com sucesso.", typeof(ResetPasswordResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]

    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    /// <summary>
    /// Rota responsável por atualizar um usuário.
    /// </summary>
    /// <param name="request">Formulário de atualização de usuário.</param>
    /// <param name="cancellationToken"></param>
    [Authorize]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Usuário atualizado com sucesso.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Usuário não enviado corretamente.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Usuário não encontrado.", typeof(CustomProblemDetails))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Erro de validação nos dados da requisição.", typeof(ValidationProblemDetails))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundProblemDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(ValidationProblemDetailsExample))]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status);
    }
}