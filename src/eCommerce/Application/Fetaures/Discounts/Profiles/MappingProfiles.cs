using Application.Fetaures.Discounts.Commands.Create;
using Application.Fetaures.Discounts.Commands.Delete;
using Application.Fetaures.Discounts.Commands.Update;
using Application.Fetaures.Discounts.Queries.GetById;
using Application.Fetaures.Discounts.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.Discounts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateDiscountCommand, Discount>();
        CreateMap<Discount, CreatedDiscountResponse>();

        CreateMap<UpdateDiscountRequest, Discount>();
        CreateMap<Discount, UpdatedDiscountResponse>();

        CreateMap<DeleteDiscountCommand, Discount>();

        CreateMap<Discount, GetByIdDiscountResponse>();

        CreateMap<Discount, GetListDiscountListItemDto>();
    }
}