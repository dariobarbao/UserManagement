using UserManagement.Application.Common.DTOs;

namespace UserManagement.Application.Features.UserFeatures.CreateUser;

public sealed record CreateUserResponse(UserReadDto User);
