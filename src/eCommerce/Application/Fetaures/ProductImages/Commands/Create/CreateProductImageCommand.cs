using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.ProductImages.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.ProductImages.Commands.Create;

public class CreateProductImageCommand : IRequest<CreatedProductImageResponse>, ITransactionalRequest
{
    public required Guid ProductId { get; set; }
    public required string ImageUrl { get; set; }

    public class CreateProductImageCommandHandler(IMapper mapper, IProductImageRepository productImageRepository) : IRequestHandler<CreateProductImageCommand, CreatedProductImageResponse>
    {
        public async Task<CreatedProductImageResponse> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            ProductImage productImage = mapper.Map<ProductImage>(request);

            await productImageRepository.AddAsync(productImage);

            CreatedProductImageResponse response = mapper.Map<CreatedProductImageResponse>(productImage);
            return response;
        }
    }
}