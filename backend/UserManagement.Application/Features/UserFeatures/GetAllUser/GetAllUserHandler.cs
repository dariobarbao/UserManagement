using AutoMapper;
using UserManagement.Application.Repositories;
using MediatR;
using UserManagement.Application.Common.DTOs;

namespace UserManagement.Application.Features.UserFeatures.GetAllUser;

public class GetAllUserHandler : IRequestHandler<GetAllUserRequest, GetAllUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetAllUserResponse> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAll(cancellationToken);

        var dtoList = _mapper.Map<List<UserReadDto>>(users);

        return new GetAllUserResponse(dtoList);
    }
}
