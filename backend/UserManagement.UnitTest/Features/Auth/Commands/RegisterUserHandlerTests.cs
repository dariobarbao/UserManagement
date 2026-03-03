using FluentAssertions;
using Moq;
using UserManagement.Application.Features.Auth.Commands.RegisterUser;
using UserManagement.Application.Repositories;
using UserManagement.Application.Common.Security;
using UserManagement.Domain.Entities;
using Xunit;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace UserManagement.UnitTest.Features.Auth.Commands;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly RegisterUserHandler _handler;

    public RegisterUserHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new RegisterUserHandler(
            _unitOfWorkMock.Object,
            _userRepositoryMock.Object,
            _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_User_And_Commit_When_Email_Does_Not_Exist()
    {
        var command = new RegisterUser
        {
            Email = "test@email.com",
            Password = "Password123"
        };

        _userRepositoryMock
            .Setup(r => r.ExistsByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHasherMock
            .Setup(h => h.Hash(command.Password))
            .Returns("hashed");

        await _handler.Handle(command, CancellationToken.None);

        _userRepositoryMock.Verify(r => r.Create(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Save(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Email_Already_Exists()
    {
        // Arrange
        var command = new RegisterUser
        {
            Email = "existing@email.com",
            Password = "Password123"
        };

        _userRepositoryMock
            .Setup(r => r.ExistsByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ApplicationException>()
            .WithMessage("*registrado*");

        _userRepositoryMock.Verify(r => r.Create(It.IsAny<User>()), Times.Never);
    }
}
