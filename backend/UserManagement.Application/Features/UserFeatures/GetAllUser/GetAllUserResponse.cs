using UserManagement.Application.Common.DTOs;

namespace UserManagement.Application.Features.UserFeatures.GetAllUser;

public record GetAllUserResponse(List<UserReadDto> Users);