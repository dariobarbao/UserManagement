using AutoMapper;
using UserManagement.Application.Common.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.GetAllUser;

public sealed class GetAllUserMapper : Profile
{
    public GetAllUserMapper()
    {
        CreateMap<User, UserReadDto>();
    }
}