using MediatR;

namespace UserManagement.Application.Features.UserFeatures.GetUserById;

public record GetUserByIdRequest : IRequest<GetUserByIdResponse>
{
    public Guid Id { get; set; }
}
