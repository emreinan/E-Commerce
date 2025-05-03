using AutoMapper;
using Domain.Entities;
using Application.Fetaures.ProductImages.Commands.Create;
using Application.Fetaures.ProductImages.Commands.Delete;
using Application.Fetaures.ProductImages.Queries.GetById;
using Application.Fetaures.ProductImages.Queries.GetList;
using Application.Fetaures.ProductImages.Commands.Update;

namespace Application.Fetaures.ProductImages.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductImageCommand, ProductImage>();
        CreateMap<ProductImage, CreatedProductImageResponse>();

        CreateMap<UpdateProductImageCommand, ProductImage>();
        CreateMap<ProductImage, UpdatedProductImageResponse>();

        CreateMap<DeleteProductImageCommand, ProductImage>();

        CreateMap<ProductImage, GetByIdProductImageResponse>();

        CreateMap<ProductImage, GetListProductImageListItemDto>();
    }
}