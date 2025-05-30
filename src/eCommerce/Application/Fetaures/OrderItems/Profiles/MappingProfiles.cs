using Application.Fetaures.OrderItems.Queries.GetById;
using Application.Fetaures.OrderItems.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.OrderItems.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OrderItem, GetByIdOrderItemResponse>();

        CreateMap<OrderItem, GetListOrderItemListItemDto>();
    }
}