#nullable enable

using Microsoft.AspNetCore.Mvc;

namespace FoundFlow.WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("api/[controller]")]
[ApiController]
public class ErrorsController : ControllerBase
{
    [HttpGet("Error")]
    public IActionResult Error()
    {
        return Problem();
    }
}