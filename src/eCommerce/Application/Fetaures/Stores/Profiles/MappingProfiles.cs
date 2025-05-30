using AutoMapper;
using Domain.Entities;
using Application.Fetaures.Stores.Commands.Update;
using Application.Fetaures.Stores.Commands.Create;
using Application.Fetaures.Stores.Commands.Delete;
using Application.Fetaures.Stores.Queries.GetById;
using Application.Fetaures.Stores.Queries.GetList;

namespace Application.Fetaures.Stores.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateStoreCommand, Store>()
            .ForMember(dest => dest.LogoUrl, opt => opt.Ignore());
        CreateMap<Store, CreatedStoreResponse>();

        CreateMap<UpdateStoreRequest, Store>();
        CreateMap<Store, UpdatedStoreResponse>();

        CreateMap<DeleteStoreCommand, Store>();

        CreateMap<Store, GetByIdStoreResponse>();

        CreateMap<Store, GetListStoreListItemDto>();
    }
}