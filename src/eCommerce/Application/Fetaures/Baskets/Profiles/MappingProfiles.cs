using Application.Fetaures.Baskets.Commands.Create;
using Application.Fetaures.Baskets.Commands.Delete;
using Application.Fetaures.Baskets.Queries.GetById;
using Application.Fetaures.Baskets.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.Baskets.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBasketCommand, Basket>();
        CreateMap<Basket, CreatedBasketResponse>();

        CreateMap<DeleteBasketCommand, Basket>();

        CreateMap<Basket, GetByIdBasketResponse>();

        CreateMap<Basket, GetListBasketListItemDto>();
    }
}