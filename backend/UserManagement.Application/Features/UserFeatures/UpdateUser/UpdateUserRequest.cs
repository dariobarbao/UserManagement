using MediatR;
using System.Text.Json.Serialization;

namespace UserManagement.Application.Features.UserFeatures.UpdateUser;
    public sealed record UpdateUserRequest(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    [property: JsonPropertyName("password")] string PasswordHash,
    bool IsActive
) : IRequest<UpdateUserResponse>;
