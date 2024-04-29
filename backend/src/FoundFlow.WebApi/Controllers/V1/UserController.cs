using System;
using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
using FoundFlow.Application.Common.Feature.Users.Login;
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

    [HttpPost("login")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetToken([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result);
    }

    [Authorize]
    [HttpGet("block")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public IActionResult Get(CancellationToken cancellationToken = default)
    {
        return StatusCode(200, "Blocked");
    }

    [AllowAnonymous]
    [HttpGet("testing")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public IActionResult GetAllow(CancellationToken cancellationToken = default)
    {
#pragma warning disable S3928
        throw new ArgumentNullException("teste");
#pragma warning restore S3928
    }
}