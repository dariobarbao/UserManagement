using MediatR;
using UserManagement.Application.Common.DTOs;
using UserManagement.Application.Common.Security;
using UserManagement.Application.Common.Services;
using UserManagement.Application.Features.Auth.Queries.Login;
using UserManagement.Application.Repositories;

public class LoginQueryHandler
    : IRequestHandler<LoginQuery, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public LoginQueryHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null ||
            !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var token = _jwtService.GenerateToken(user, out var expiresAt);

        return new AuthResponse
        {
            Token = token,
            ExpiresAt = expiresAt
        };
    }
}
