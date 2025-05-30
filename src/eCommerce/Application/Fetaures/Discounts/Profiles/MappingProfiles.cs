using Application.Fetaures.Discounts.Commands.Apply;
using Application.Fetaures.Discounts.Commands.Create;
using Application.Fetaures.Discounts.Commands.Delete;
using Application.Fetaures.Discounts.Commands.Update;
using Application.Fetaures.Discounts.Queries.GetByCode;
using Application.Fetaures.Discounts.Queries.GetById;
using Application.Fetaures.Discounts.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Fetaures.Discounts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<DiscountType, string>().ConvertUsing<DiscountTypeToStringConverter>();

        CreateMap<CreateDiscountCommand, Discount>();
        CreateMap<Discount, CreatedDiscountResponse>();

        CreateMap<UpdateDiscountRequest, Discount>();
        CreateMap<Discount, UpdatedDiscountResponse>();

        CreateMap<DeleteDiscountCommand, Discount>();
        CreateMap<Discount, GetByIdDiscountResponse>() ;

        CreateMap<Discount, GetByCodeDiscountResponse>() ;

        CreateMap<Discount, AppliedDiscountResponse>()
            .ForMember(dest => dest.TotalDiscountValue, opt => opt.Ignore())
            .ForMember(dest => dest.NewTotal, opt => opt.Ignore());

        CreateMap<Discount, GetListDiscountListItemDto>();
    }
}
public class DiscountTypeToStringConverter : ITypeConverter<DiscountType, string>
{
    public string Convert(DiscountType source, string destination, ResolutionContext context)
        => source.ToString();
}
