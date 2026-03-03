using AutoMapper;
using UserManagement.Application.Common.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.UpdateUser;

public sealed class UpdateUserMapper : Profile
{
    public UpdateUserMapper()
    {
        CreateMap<UpdateUserRequest, User>();
        CreateMap<User, UserReadDto>();
    }
}