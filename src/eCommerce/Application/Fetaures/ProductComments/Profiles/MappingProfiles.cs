using Application.Fetaures.ProductComments.Commands.Create;
using Application.Fetaures.ProductComments.Commands.Delete;
using Application.Fetaures.ProductComments.Commands.Update;
using Application.Fetaures.ProductComments.Queries.GetById;
using Application.Fetaures.ProductComments.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.ProductComments.Profiles;

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