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
        CreateMap<BasketItem, CreatedBasketItemResponse>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));

        CreateMap<UpdateBasketItemRequest, BasketItem>();
        CreateMap<BasketItem, UpdatedBasketItemResponse>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));

        CreateMap<DeleteBasketItemCommand, BasketItem>();

        CreateMap<BasketItem, GetByIdBasketItemResponse>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));

        CreateMap<BasketItem, GetListBasketItemListItemDto>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice));
    }
}