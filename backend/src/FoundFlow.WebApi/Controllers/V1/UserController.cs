using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Users.Create;
using FoundFlow.Application.Common.Feature.Users.Login;
using MediatR;
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
}