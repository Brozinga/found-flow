#nullable enable

using Microsoft.AspNetCore.Mvc;

namespace FoundFlow.WebApi.Controllers;

[Route("/error")]
public class ErrorsController : ControllerBase
{
    public IActionResult Error()
    {
        return Problem();
    }
}