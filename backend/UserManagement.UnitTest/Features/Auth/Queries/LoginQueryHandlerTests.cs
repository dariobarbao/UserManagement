using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common.Security;
using UserManagement.Application.Common.Services;
using UserManagement.Application.Features.Auth.Queries.Login;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;
using Xunit;

namespace UserManagement.UnitTest.Features.Auth.Queries;

public class LoginQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly LoginQueryHandler _handler;

    public LoginQueryHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _jwtServiceMock = new Mock<IJwtService>();

        _handler = new LoginQueryHandler(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _jwtServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Token_When_Credentials_Are_Valid()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@email.com",
            PasswordHash = "hashed-password"
        };

        var query = new LoginQuery
        {
            Email = user.Email,
            Password = "Password123"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(user.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(h => h.Verify(query.Password, user.PasswordHash))
            .Returns(true);

        _jwtServiceMock
            .Setup(j => j.GenerateToken(It.IsAny<User>(), out It.Ref<DateTime>.IsAny))
            .Returns((User u, out DateTime expiresAt) =>
            {
                expiresAt = DateTime.UtcNow.AddMinutes(60);
                return "jwt-token";
            });

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Token.Should().Be("jwt-token");
        result.ExpiresAt.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public async Task Handle_Should_Throw_Unauthorized_When_Password_Is_Invalid()
    {
        // Arrange
        var user = new User
        {
            Email = "test@email.com",
            PasswordHash = "hashed-password"
        };

        var query = new LoginQuery
        {
            Email = user.Email,
            Password = "wrong-password"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(user.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(h => h.Verify(query.Password, user.PasswordHash))
            .Returns(false);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task Handle_Should_Throw_Unauthorized_When_User_Does_Not_Exist()
    {
        // Arrange
        var query = new LoginQuery
        {
            Email = "notfound@email.com",
            Password = "Password123"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(query.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
