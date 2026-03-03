using MediatR;
using System.Text.Json.Serialization;
using UserManagement.Application.Common.DTOs;

namespace UserManagement.Application.Features.UserFeatures.CreateUser;

public sealed record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    [property: JsonPropertyName("password")] string PasswordHash
) : IRequest<CreateUserResponse>;