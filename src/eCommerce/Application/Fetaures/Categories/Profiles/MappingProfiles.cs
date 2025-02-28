using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetById;
using Application.Features.Categories.Queries.GetList;
using Application.Fetaures.Categories.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Categories.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<Category, CreatedCategoryResponse>();

        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<Category, UpdatedCategoryResponse>();

        CreateMap<DeleteCategoryCommand, Category>();

        CreateMap<Category, GetByIdCategoryResponse>();

        CreateMap<Category, GetListCategoryListItemDto>();
    }
}