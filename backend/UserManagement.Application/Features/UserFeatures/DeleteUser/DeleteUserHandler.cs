using AutoMapper;
using MediatR;
using UserManagement.Application.Common.DTOs;
using UserManagement.Application.Common.Exceptions;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.DeleteUser;

public sealed class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteUserHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(
            request.Id,
            cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found");

        _userRepository.Delete(user);
        await _unitOfWork.Save(cancellationToken);

        var userDto = _mapper.Map<UserReadDto>(user);
        return new DeleteUserResponse(userDto);
    }
}