using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.Auth.Commands.RegisterUser;
using UserManagement.Application.Features.Auth.Queries.Login;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUser command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Register), new { id = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
