using MediatR;

namespace UserManagement.Application.Features.UserFeatures.GetAllUser;

public record GetAllUserRequest : IRequest<GetAllUserResponse>;