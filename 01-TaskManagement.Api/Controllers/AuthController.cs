using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities.Commands.AuthCommands;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("Authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] AuthCommand authCommand)
    {
        var objResponse = await _mediator.Send(authCommand);

        if (!objResponse.Success)
            return CreatedAtAction(nameof(Authenticate), new { id = 0 }, objResponse);
            
        return Ok(objResponse);
    }

    [HttpGet]
    [Route("GetServerTime")]
    [AllowAnonymous]
    public ObjectResult GetServerTime()
    {
        return Ok(DateTime.UtcNow);
    }
}