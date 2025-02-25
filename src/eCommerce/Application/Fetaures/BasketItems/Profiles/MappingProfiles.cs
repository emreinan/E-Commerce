using Application.Fetaures.BasketItems.Commands.Create;
using Application.Fetaures.BasketItems.Commands.Delete;
using Application.Fetaures.BasketItems.Commands.Update;
using Application.Fetaures.BasketItems.Queries.GetById;
using Application.Fetaures.BasketItems.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.BasketItems.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBasketItemCommand, BasketItem>(); 
        CreateMap<BasketItem, CreatedBasketItemResponse>();

        CreateMap<UpdateBasketItemRequest, BasketItem>();
        CreateMap<BasketItem, UpdatedBasketItemResponse>();

        CreateMap<DeleteBasketItemCommand, BasketItem>();

        CreateMap<BasketItem, GetByIdBasketItemResponse>();

        CreateMap<BasketItem, GetListBasketItemListItemDto>();
    }
}