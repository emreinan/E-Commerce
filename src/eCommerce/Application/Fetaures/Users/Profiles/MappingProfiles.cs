using Application.Fetaures.Users.Commands.Create;
using Application.Fetaures.Users.Commands.Delete;
using Application.Fetaures.Users.Commands.Update;
using Application.Fetaures.Users.Dtos;
using Application.Fetaures.Users.Queries.GetById;
using Application.Fetaures.Users.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>();
        CreateMap<User, CreatedUserResponse>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, GetByIdUserResponse>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();

        CreateMap<UpdateUserRequest, User>()
            .ForMember(dest => dest.PersonalInfo, opt => opt.MapFrom(src => src.PersonalInfo));

        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.PersonalInfo, opt => opt.MapFrom(src => src.PersonalInfo));

        CreateMap<PersonalInfoDto, PersonalInfo>();
    }
}
