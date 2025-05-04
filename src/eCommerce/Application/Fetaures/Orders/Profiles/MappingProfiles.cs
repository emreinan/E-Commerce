using Application.Fetaures.Orders.Commands.Create;
using Application.Fetaures.Orders.Commands.Delete;
using Application.Fetaures.Orders.Commands.Update;
using Application.Fetaures.Orders.Queries.GetById;
using Application.Fetaures.Orders.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.Orders.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreatedOrderResponse>();

        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<Order, UpdatedOrderResponse>();

        CreateMap<DeleteOrderCommand, Order>();

        CreateMap<Order, GetByIdOrderResponse>();

        CreateMap<Order, GetListOrderListItemDto>();
    }
}