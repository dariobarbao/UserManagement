using AutoMapper;
using UserManagement.Application.Common.DTOs;
using UserManagement.Application.Features.UserFeatures.UpdateUser;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.DeleteUser;

public sealed class DeleteUserMapper : Profile
{
    public DeleteUserMapper()
    {
        CreateMap<User, UserReadDto>();
    }
}
