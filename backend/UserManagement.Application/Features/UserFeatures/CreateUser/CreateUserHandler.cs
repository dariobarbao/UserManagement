using AutoMapper;
using MediatR;
using UserManagement.Application.Common.DTOs;
using UserManagement.Application.Common.Security;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public CreateUserHandler(
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

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken);
        if (userExists)
            throw new InvalidOperationException($"Ya existe un usuario registrado con el email: " + request.Email);


        var user = _mapper.Map<User>(request);
        user.CreatedAt = DateTime.UtcNow;
        user.IsActive = true;
        user.PasswordHash = _passwordHasher.Hash(request.PasswordHash);
        _userRepository.Create(user);
        await _unitOfWork.Save(cancellationToken);

        var userDto = _mapper.Map<UserReadDto>(user);
        return new CreateUserResponse(userDto);
    }
}