using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.ProductImages.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdateProductImageCommand : IRequest<UpdatedProductImageResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateProductImageRequest Request { get; set; } = default!;

    public class UpdateProductImageCommandHandler(IMapper mapper, IProductImageRepository productImageRepository,
                                     ProductImageBusinessRules productImageBusinessRules) : IRequestHandler<UpdateProductImageCommand, UpdatedProductImageResponse>
    {
        public async Task<UpdatedProductImageResponse> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            ProductImage? productImage = await productImageRepository.GetAsync(predicate: pi => pi.Id == request.Id, cancellationToken: cancellationToken);
            productImageBusinessRules.ProductImageShouldExistWhenSelected(productImage);
            productImage = mapper.Map(request.Request, productImage);

            await productImageRepository.UpdateAsync(productImage!, cancellationToken: cancellationToken);

            UpdatedProductImageResponse response = mapper.Map<UpdatedProductImageResponse>(productImage);
            return response;
        }
    }
}