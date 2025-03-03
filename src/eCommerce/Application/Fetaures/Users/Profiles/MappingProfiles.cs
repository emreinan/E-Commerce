using Application.Fetaures.Users.Commands.Create;
using Application.Fetaures.Users.Commands.Delete;
using Application.Fetaures.Users.Commands.Update;
using Application.Fetaures.Users.Queries.GetById;
using Application.Fetaures.Users.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, GetByIdUserResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
    }
}
