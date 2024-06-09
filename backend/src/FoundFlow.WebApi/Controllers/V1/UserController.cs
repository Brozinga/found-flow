using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Users.Create;
using FoundFlow.Application.Common.Feature.Users.Login;
using FoundFlow.Application.Common.Feature.Users.ResetPassword;
using FoundFlow.Application.Common.Feature.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoundFlow.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Route("user")]
public class UserController : BaseController<UserController>
{
    public UserController(ISender sender, ILogger<UserController> logger)
        : base(sender, logger)
    {
    }

    /// <summary>
    /// Rota responsável por retornar o Bearer Token para acesso as Rotas bloqueadas.
    /// </summary>
    /// <param name="request">Formulário de login.</param>
    /// <param name="cancellationToken"></param>
    [HttpPost("login")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetToken([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result.Data);
    }

    /// <summary>
    /// Rota responsável por registrar um novo usuário.
    /// </summary>
    /// <param name="request">Formulário de cadastro.</param>
    /// <param name="cancellationToken"></param>
    [HttpPost("register")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
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
    [ProducesResponseType(typeof(ResetPasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status);
    }
}