using AutoMapper;
using Domain.Entities;
using Application.Fetaures.Addresses.Queries.GetList;
using Application.Fetaures.Addresses.Commands.Update;
using Application.Fetaures.Addresses.Commands.Delete;
using Application.Fetaures.Addresses.Commands.Create;
using Application.Fetaures.Addresses.Queries.GetById;

namespace Application.Fetaures.Addresses.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateAddressCommand, Address>();
        CreateMap<Address, CreatedAddressResponse>();

        CreateMap<UpdateAddressRequest, Address>();
        CreateMap<Address, UpdatedAddressResponse>();

        CreateMap<DeleteAddressCommand, Address>();

        CreateMap<Address, GetByIdAddressResponse>();

        CreateMap<Address, GetListAddressListItemDto>();
    }
}