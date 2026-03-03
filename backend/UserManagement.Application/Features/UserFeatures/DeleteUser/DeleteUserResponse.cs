using UserManagement.Application.Common.DTOs;

namespace UserManagement.Application.Features.UserFeatures.DeleteUser;

public sealed record DeleteUserResponse(UserReadDto User);
