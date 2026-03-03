

using MediatR;

namespace UserManagement.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUser : IRequest<Guid>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
