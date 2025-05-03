using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.ProductImages.Rules;

namespace Application.Fetaures.ProductImages.Queries.GetById;

public class GetByIdProductImageQuery : IRequest<GetByIdProductImageResponse>
{
    public Guid Id { get; set; }

    public class GetByIdProductImageQueryHandler(IMapper mapper, IProductImageRepository productImageRepository, ProductImageBusinessRules productImageBusinessRules) : IRequestHandler<GetByIdProductImageQuery, GetByIdProductImageResponse>
    {
        public async Task<GetByIdProductImageResponse> Handle(GetByIdProductImageQuery request, CancellationToken cancellationToken)
        {
            ProductImage? productImage = await productImageRepository.GetAsync(predicate: pi => pi.Id == request.Id, cancellationToken: cancellationToken);
            productImageBusinessRules.ProductImageShouldExistWhenSelected(productImage);

            GetByIdProductImageResponse response = mapper.Map<GetByIdProductImageResponse>(productImage);
            return response;
        }
    }
}