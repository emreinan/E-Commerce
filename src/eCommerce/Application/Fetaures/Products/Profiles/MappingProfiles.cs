using Application.Fetaures.Products.Commands.Create;
using Application.Fetaures.Products.Commands.Delete;
using Application.Fetaures.Products.Commands.Update;
using Application.Fetaures.Products.Queries.GetById;
using Application.Fetaures.Products.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.Products.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreatedProductResponse>();

        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, UpdatedProductResponse>();

        CreateMap<DeleteProductCommand, Product>();

        CreateMap<Product, GetByIdProductResponse>();

        CreateMap<Product, GetListProductListItemDto>();
    }
}