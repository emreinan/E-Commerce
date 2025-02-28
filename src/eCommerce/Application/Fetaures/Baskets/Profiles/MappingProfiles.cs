using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Queries.GetById;
using Application.Features.Baskets.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Baskets.Profiles;

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