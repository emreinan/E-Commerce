using Application.Features.ProductComments.Commands.Create;
using Application.Features.ProductComments.Commands.Delete;
using Application.Features.ProductComments.Commands.Update;
using Application.Features.ProductComments.Queries.GetById;
using Application.Features.ProductComments.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.ProductComments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProductCommentCommand, ProductComment>();
        CreateMap<ProductComment, CreatedProductCommentResponse>();

        CreateMap<UpdateProductCommentRequest, ProductComment>();
        CreateMap<ProductComment, UpdatedProductCommentResponse>();

        CreateMap<DeleteProductCommentCommand, ProductComment>();

        CreateMap<ProductComment, GetByIdProductCommentResponse>();

        CreateMap<ProductComment, GetListProductCommentListItemDto>();
    }
}