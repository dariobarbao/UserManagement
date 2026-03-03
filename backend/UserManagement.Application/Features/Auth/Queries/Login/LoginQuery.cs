

using MediatR;
using UserManagement.Application.Common.DTOs;

namespace UserManagement.Application.Features.Auth.Queries.Login;

public class LoginQuery : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
