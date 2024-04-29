using System.Threading;
using System.Threading.Tasks;
using Asp.Versioning;
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

    [HttpPost]
    [Route("login")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Get([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Sender.Send(request, cancellationToken);
        return StatusCode(result.Status, result);
    }
}