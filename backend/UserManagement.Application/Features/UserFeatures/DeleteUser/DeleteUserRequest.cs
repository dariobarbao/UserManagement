using MediatR;

namespace UserManagement.Application.Features.UserFeatures.DeleteUser;

public record DeleteUserRequest(Guid Id)
    : IRequest<DeleteUserResponse>;
