using Application.Fetaures.Categories.Commands.Create;
using Application.Fetaures.Categories.Commands.Delete;
using Application.Fetaures.Categories.Commands.Update;
using Application.Fetaures.Categories.Queries.GetById;
using Application.Fetaures.Categories.Queries.GetList;
using AutoMapper;
using Domain.Entities;

namespace Application.Fetaures.Categories.Profiles;

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