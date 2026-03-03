using AutoMapper;
using UserManagement.Application.Common.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserMapper : Profile
{
    public CreateUserMapper()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, UserReadDto>();
    }
}