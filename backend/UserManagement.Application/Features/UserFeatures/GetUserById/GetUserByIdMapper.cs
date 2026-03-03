using AutoMapper;
using UserManagement.Application.Common.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.UserFeatures.GetUserById;

public sealed class GetUserByIdMapper : Profile
{
    public GetUserByIdMapper()
    {
        CreateMap<User, UserReadDto>();
    }
}
