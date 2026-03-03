using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Features.UserFeatures.CreateUser;
using UserManagement.Application.Features.UserFeatures.DeleteUser;
using UserManagement.Application.Features.UserFeatures.GetAllUser;
using UserManagement.Application.Features.UserFeatures.GetUserById;
using UserManagement.Application.Features.UserFeatures.UpdateUser;

namespace UserManagement.WebAPI.Controllers;


[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserByIdResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetUserByIdRequest { Id = id}, cancellationToken);
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateUserResponse>> Update(
        Guid id,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var requestWithId = request with { Id = id };
        var response = await _mediator.Send(requestWithId, cancellationToken);
        return Ok(response);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<DeleteUserResponse>> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest(id);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}