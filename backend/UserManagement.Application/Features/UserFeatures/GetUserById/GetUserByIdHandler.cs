using AutoMapper;
using MediatR;
using UserManagement.Application.Common.DTOs;
using UserManagement.Application.Common.Exceptions;
using UserManagement.Application.Features.UserFeatures.GetAllUser;
using UserManagement.Application.Repositories;

namespace UserManagement.Application.Features.UserFeatures.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found");

        var dto = _mapper.Map<UserReadDto>(user);

        return new GetUserByIdResponse(dto);
    }
}
