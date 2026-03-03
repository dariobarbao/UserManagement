using AutoMapper;
using MediatR;
using UserManagement.Application.Common.DTOs;
using UserManagement.Application.Common.Exceptions;
using UserManagement.Application.Common.Security;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.UpdateUser;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public UpdateUserHandler(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        if (user is null)
            throw new NotFoundException("User not found");

        user.UpdatedAt = DateTime.UtcNow;
        user.IsActive = request.IsActive;
        user.PasswordHash = _passwordHasher.Hash(request.PasswordHash);
        _userRepository.Update(user);
        await _unitOfWork.Save(cancellationToken);

        var userDto = _mapper.Map<UserReadDto>(user);
        return new UpdateUserResponse(userDto);
    }
}