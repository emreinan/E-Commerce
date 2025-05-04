using Application.Fetaures.OrderItems.Commands.Create;
using Application.Fetaures.OrderItems.Commands.Delete;
using Application.Fetaures.OrderItems.Commands.Update;
using Application.Fetaures.OrderItems.Queries.GetById;
using Application.Fetaures.OrderItems.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.OrderItems.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateOrderItemCommand, OrderItem>();
        CreateMap<OrderItem, CreatedOrderItemResponse>();

        CreateMap<UpdateOrderItemCommand, OrderItem>();
        CreateMap<OrderItem, UpdatedOrderItemResponse>();

        CreateMap<DeleteOrderItemCommand, OrderItem>();

        CreateMap<OrderItem, GetByIdOrderItemResponse>();

        CreateMap<OrderItem, GetListOrderItemListItemDto>();
    }
}